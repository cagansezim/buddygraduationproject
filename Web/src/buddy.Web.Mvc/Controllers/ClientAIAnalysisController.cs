using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using buddy.AI.Analysis;
using buddy.AI.Analysis.Dto;
using buddy.AIDiagnoses;
using buddy.AIDiagnoses.Dtos;
using buddy.Clinics;
using buddy.Pets;
using buddy.Records;
using buddy.Records.Dtos;
using buddy.Controllers;

namespace buddy.Web.Controllers
{
    [Route("ClientAIAnalysis")]
    public class ClientAIAnalysisController : buddyControllerBase
    {
        private readonly IAnalysisAppService _analysis;
        private readonly IPetAppService _pets;
        private readonly IClinicAppService _clinics;
        private readonly IAIDiagnosisAppService _aiDiag;
        private readonly IRecordAppService _records;
        private readonly IAbpSession _session;

        private const int DummyClinicId = 1;   // fallback defaults
        private const int DummyVetId = 1;

        /* ---------------- constructor ---------------------- */
        public ClientAIAnalysisController(
            IAnalysisAppService analysis,
            IPetAppService pets,
            IClinicAppService clinics,
            IAIDiagnosisAppService aiDiag,
            IRecordAppService records,
            IAbpSession session)
        {
            _analysis = analysis;
            _pets = pets;
            _clinics = clinics;
            _aiDiag = aiDiag;
            _records = records;
            _session = session;
        }

        /* ---------------- LANDING -------------------------- */
        [HttpGet("Index")] public async Task<IActionResult> Index() { await FillBagsAsync(); return View(); }
        [HttpGet("Create")] public async Task<IActionResult> Create() { await FillBagsAsync(); return View(); }

        /* ---------------- STEP‑1 : analyse ----------------- */
        [HttpPost("Analyze")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Analyze(IFormCollection form, List<IFormFile> images)
        {
            await FillBagsAsync();

            string prompt = form["Prompt"];
            string duration = form["Duration"];
            string comments = form["Comments"];
            int? petId = int.TryParse(form["PetId"], out var id) ? id : null;

            var imgBytes = new List<byte[]>();
            var mimeList = new List<string>();
            foreach (var f in images.Where(x => x.Length > 0))
            {
                await using var ms = new MemoryStream();
                await f.CopyToAsync(ms);
                imgBytes.Add(ms.ToArray());
                mimeList.Add(f.ContentType);
            }

            if (string.IsNullOrWhiteSpace(prompt) && imgBytes.Count == 0)
            {
                ModelState.AddModelError("", "Describe symptoms or attach an image.");
                return View("Create");
            }

            /* ---- call Gemini / GPT ---- */
            var raw = await _analysis.AnalyzeAsync(new AnalysisInput
            {
                Prompt = prompt,
                Duration = duration,
                PetType = "CAT_OR_DOG", // stub
                Comments = comments,
                ImageBytesList = imgBytes,
                MimeTypes = mimeList,
                SystemInstruction = "Analyse pet symptoms & media",
                Temperature = 0.3f
            });
            var parsed = ParseSections(raw);

            var vm = new ClientAIAnalysisResultDto
            {
                Prompt = prompt,
                Duration = duration,
                Comments = comments,
                PetType = "CAT_OR_DOG",
                PetId = petId,
                ImageBytesList = imgBytes,
                MimeTypes = mimeList,
                Date = DateTime.UtcNow,
                PossibleCauses = parsed["Possible Causes"],
                Recommendations = parsed["Recommendations"],
                WhatToTellYourVet = parsed["What to Tell Your Veterinarian"]
            };
            await Confirm(vm);

            ViewBag.AllClinics = (await _clinics.GetAllAsync(
                new PagedAndSortedResultRequestDto { MaxResultCount = 200 }))
                .Items
                .Cast<Clinics.Dtos.ClinicWithVeterinarianDto>()
                .ToList();

            return View("Results", vm);
        }

        /* ---------------- STEP‑2 : confirm & save ---------- */
        [HttpPost("Confirm")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(ClientAIAnalysisResultDto vm)
        {
            // ── sanity fallback ────────────────────────────────────────
            int clinicId = vm.ClinicId ?? DummyClinicId;
            int vetId = vm.VetUserId ?? DummyVetId;
            int ownerId = (int)_session.GetUserId();

            /* 1️⃣  Create Record first
               ---------------------------------------------------------- */
            var rec = await _records.CreateAsync(new CreateRecordDto
            {
                PetId = vm.PetId ?? 0,
                PetOwnerUserId = ownerId,
                VetUserId = vetId,
                ClinicId = clinicId,
                Name = $"AI Diagnosis {DateTime.UtcNow:yyyy-MM-dd HH:mm}",
                Description = vm.Prompt,
                ImageUrl = vm.ImageBytesList.Any()
                                 ? $"data:{vm.MimeTypes[0]};base64,{Convert.ToBase64String(vm.ImageBytesList[0])}"
                                 : null
            });

            /* 2️⃣  Seed an (empty) rating row so the FK is satisfied
               ---------------------------------------------------------- */
            var rating = await _aiDiag.CreateOrGetEmptyRatingAsync(ownerId);

            /* 3️⃣  Create AI diagnosis, point to that rating + record
               ---------------------------------------------------------- */
            var diag = await _aiDiag.CreateAsync(new CreateAIDiagnosisDto
            {
                RecordId = rec.Id,
                DiagnosisRatingId = rating.Id,                        // NEVER NULL
                Cost_Estimation = JsonConvert.SerializeObject(new
                {
                    Causes = vm.PossibleCauses,
                    Recommendations = vm.Recommendations,
                    VetInstructions = vm.WhatToTellYourVet
                })
            });

            /* 4️⃣  Patch the record with the newly‑created diagnosis FK
               ---------------------------------------------------------- */
            await _records.UpdateAsync(new UpdateRecordDto
            {
                Id = rec.Id,
                AIDiagnosisId = diag.Id
            });

            TempData["Success"] = "Analysis, record and AI diagnosis saved!";
            return RedirectToAction("MyAnalysisResults");
        }

        /* ---------------- LIST / DETAILS ------------------- */
        [HttpGet("MyAnalysisResults")]
        public async Task<IActionResult> MyAnalysisResults()
        {
            var mine = (await _records.GetAllAsync(
                        new PagedAndSortedResultRequestDto { MaxResultCount = 200 }))
                        .Items
                        .Where(r => r.PetOwnerUserId == (int)_session.GetUserId())
                        .ToList();
            return View(mine);
        }

        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var rec = await _records.GetAsync(new EntityDto<int>(id));
            var diag = rec.AIDiagnosisId.HasValue
                       ? await _aiDiag.GetAsync(new EntityDto<int>((int)rec.AIDiagnosisId))
                       : null;

            dynamic pack = diag != null ? JsonConvert.DeserializeObject<dynamic>(diag.Cost_Estimation) : null;

            var vm = new ClientAIAnalysisResultDto
            {
                Prompt = rec.Description,
                Date = rec.Created_at,
                PossibleCauses = pack?.Causes?.ToObject<List<string>>() ?? new List<string>(),
                Recommendations = pack?.Recommendations?.ToObject<List<string>>() ?? new List<string>(),
                WhatToTellYourVet = pack?.VetInstructions?.ToObject<List<string>>() ?? new List<string>()
            };
            return View(vm);
        }

        /* ---------------- helpers -------------------------- */
        private async Task FillBagsAsync()
        {
            ViewBag.UserPets = (await _pets.GetAllAsync(
                                new PagedAndSortedResultRequestDto { MaxResultCount = 100 }))
                                .Items;
        }

        private Dictionary<string, List<string>> ParseSections(string txt)
        {
            var dict = new Dictionary<string, List<string>>
            {
                ["Possible Causes"] = new(),
                ["Recommendations"] = new(),
                ["What to Tell Your Veterinarian"] = new()
            };
            string current = null;
            foreach (var line in txt.Split('\n', '\r').Select(l => l.Trim()).Where(l => l != ""))
            {
                if (dict.ContainsKey(line)) { current = line; continue; }
                if (current != null)
                {
                    var m = Regex.Match(line, @"^\d+\.\s*(.*)");
                    if (m.Success) dict[current].Add(m.Groups[1].Value.Trim());
                }
            }
            return dict;
        }
    }
}
