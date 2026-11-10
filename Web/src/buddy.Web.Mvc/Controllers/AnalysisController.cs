using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using buddy.AI.Analysis.Dto;
using buddy.AI.Analysis;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace buddy.Controllers
{
    [AbpMvcAuthorize]
    public class AnalysisController : buddyControllerBase
    {
        private readonly IAnalysisAppService _analysisAppService;

        public AnalysisController(IAnalysisAppService analysisAppService)
        {
            _analysisAppService = analysisAppService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new AnalysisInput());
        }

        [HttpPost]
        public async Task<IActionResult> Analyze([FromForm] AnalysisInput input, IFormFileCollection images)
        {
            // Handle multiple image uploads
            if (images != null && images.Count > 0)
            {
                foreach (var image in images)
                {
                    if (image.Length > 0)
                    {
                        using var stream = new MemoryStream();
                        await image.CopyToAsync(stream);
                        input.ImageBytesList.Add(stream.ToArray()); // Add each image's bytes to the list
                        input.MimeTypes.Add(image.ContentType);    // Add each image's MIME type to the list
                    }
                }
            }

            // Validate that at least one input is provided
            if (string.IsNullOrEmpty(input.Prompt) && (input.ImageBytesList == null || input.ImageBytesList.Count == 0))
            {
                ModelState.AddModelError("", "Please provide either a description or at least one image.");
                return View("Index", input);
            }

            try
            {
                // Combine survey fields into a single prompt
                string fullPrompt = $"Symptoms: {input.Prompt}\nDuration: {input.Duration}\nPet Type: {input.PetType}\nComments: {input.Comments}";
                input.Prompt = fullPrompt;

                // Set default system instruction and temperature
                input.SystemInstruction = "Analyze the images and survey to diagnose the pet's health condition.";
                if (!input.Temperature.HasValue)
                {
                    input.Temperature = 0.3f; // Default temperature
                }

                // Call the analysis service
                var result = await _analysisAppService.AnalyzeAsync(input);

                // Parse the AI response into sections
                var sections = ParseAiResponse(result);

                // Pass the sections to the view
                ViewBag.Sections = sections;
                return View("Result", input);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Analysis failed: {ex.Message}");
                return View("Index", input);
            }
        }

        [HttpGet]
        public async Task<IActionResult> TestSample()
        {
            var sampleInput = new AnalysisInput
            {
                Prompt = "My cat has been sneezing and has watery eyes for two days.",
                Duration = "2 days",
                PetType = "cat",
                Comments = "No history of allergies",
                SystemInstruction = "Analyze the images and survey to diagnose the pet's health condition.",
                Temperature = 0.3f
            };

            // Add sample images (for testing)
            var sampleImageBytes = GetSampleImageBytes();
            sampleInput.ImageBytesList.Add(sampleImageBytes);
            sampleInput.MimeTypes.Add("image/jpeg");

            try
            {
                var result = await _analysisAppService.AnalyzeAsync(sampleInput);

                // Parse the AI response into sections
                var sections = ParseAiResponse(result);

                // Pass the sections to the view
                ViewBag.Sections = sections;
                return View("Result", sampleInput);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Test failed: {ex.Message}");
                return BadRequest($"Test failed: {ex.Message}");
            }
        }

        private byte[] GetSampleImageBytes()
        {
            var sampleImagePath = @"C:\Users\cagan\Desktop\1.jpg";
            if (!System.IO.File.Exists(sampleImagePath))
            {
                throw new FileNotFoundException("Sample image not found at: " + sampleImagePath);
            }
            return System.IO.File.ReadAllBytes(sampleImagePath);
        }

        private Dictionary<string, object> ParseAiResponse(string response)
        {
            var sections = new Dictionary<string, object>();
            var lines = response.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
            string currentSection = null;
            List<string> numberedItems = null;

            // Define expected section headers
            var expectedSections = new HashSet<string>
            {
                "Symptoms Analysis",
                "Possible Causes",
                "Recommendations",
                "What to Tell Your Veterinarian"
            };

            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine)) continue; // Skip empty lines

                // Check for section headers (plain text)
                if (expectedSections.Contains(trimmedLine))
                {
                    // Save the previous section if it exists
                    if (currentSection != null)
                    {
                        if (currentSection == "Possible Causes" || currentSection == "Recommendations" || currentSection == "What to Tell Your Veterinarian")
                        {
                            sections[currentSection] = numberedItems ?? new List<string>();
                        }
                        else
                        {
                            sections[currentSection] = sections[currentSection]?.ToString().Trim() ?? "";
                        }
                    }

                    // Start a new section
                    currentSection = trimmedLine;
                    if (currentSection == "Possible Causes" || currentSection == "Recommendations" || currentSection == "What to Tell Your Veterinarian")
                    {
                        numberedItems = new List<string>();
                        sections[currentSection] = numberedItems;
                    }
                    else
                    {
                        sections[currentSection] = "";
                    }
                }
                else if (currentSection != null)
                {
                    // Handle numbered items for Possible Causes, Recommendations, and What to Tell Your Veterinarian
                    if (currentSection == "Possible Causes" || currentSection == "Recommendations" || currentSection == "What to Tell Your Veterinarian")
                    {
                        var match = Regex.Match(trimmedLine, @"^\d+\.\s*(.*)");
                        if (match.Success)
                        {
                            string item = match.Groups[1].Value.Trim().Replace("**", "");
                            numberedItems.Add(item);
                        }
                    }
                    else
                    {
                        // For non-list sections, concatenate the lines
                        string cleanedLine = trimmedLine.Replace("**", "");
                        sections[currentSection] = (sections[currentSection]?.ToString() ?? "") + cleanedLine + "\n";
                    }
                }
            }

            // Save the last section
            if (currentSection != null)
            {
                if (currentSection == "Possible Causes" || currentSection == "Recommendations" || currentSection == "What to Tell Your Veterinarian")
                {
                    sections[currentSection] = numberedItems ?? new List<string>();
                }
                else
                {
                    sections[currentSection] = sections[currentSection]?.ToString().Trim() ?? "";
                }
            }

            return sections;
        }
    }
}