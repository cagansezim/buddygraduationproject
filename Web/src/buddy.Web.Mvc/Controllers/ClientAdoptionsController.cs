using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using buddy.Authorization;
using buddy.Web.Controllers;
using buddy.Adoptions;
using buddy.Adoptions.Dtos;
using buddy.Pets;
using AutoMapper.Internal.Mappers;
using buddy.Controllers;
using System.Web.Http.ModelBinding;

namespace buddy.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Adoptions)]
    public class ClientAdoptionsController : buddyControllerBase
    {
        private readonly IAdoptionAppService _adoptionAppService;
        private readonly IPetAppService _petAppService;

        public ClientAdoptionsController(
            IAdoptionAppService adoptionAppService,
            IPetAppService petAppService)
        {
            _adoptionAppService = adoptionAppService;
            _petAppService = petAppService;
        }

        // GET: Index (filtered adoption listings)
        public async Task<IActionResult> Index(string Keyword = null, string Species = null, string Sex = null, int? MaxAge = null, double? MaxWeight = null, int page = 1, int pageSize = 10)
        {
            var input = new PagedAdoptionResultRequestDto
            {
                Keyword = Keyword,
                Species = Species,
                Sex = Sex,
                MaxAge = MaxAge,
                MaxWeight = MaxWeight,
                SkipCount = (page - 1) * pageSize,
                MaxResultCount = pageSize
            };

            var adoptions = await _adoptionAppService.GetAllAsync(input);
            foreach (var adoption in adoptions.Items)
            {
                if (adoption.Image != null && adoption.Image.Length > 0)
                    adoption.ImageBase64 = Convert.ToBase64String(adoption.Image);
            }

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = adoptions.TotalCount;
            ViewBag.Keyword = Keyword;
            ViewBag.Species = Species;
            ViewBag.Sex = Sex;
            ViewBag.MaxAge = MaxAge ?? 20;
            ViewBag.MaxWeight = MaxWeight ?? 100;

            return View("Adoptions", adoptions);
        }

        // GET: MyAdoptions (current user's listings)
        public async Task<IActionResult> MyAdoptions()
        {
            var adoptions = await _adoptionAppService.GetUserAdoptions();
            foreach (var adoption in adoptions.Items)
            {
                if (adoption.Image != null && adoption.Image.Length > 0)
                    adoption.ImageBase64 = Convert.ToBase64String(adoption.Image);
            }
            return View(adoptions);
        }

        // GET: Details of a single adoption
        public async Task<IActionResult> Details(int id)
        {
            var adoption = await _adoptionAppService.GetAsync(new EntityDto<int>(id));
            if (adoption.Image != null && adoption.Image.Length > 0)
                adoption.ImageBase64 = Convert.ToBase64String(adoption.Image);
            return View(adoption);
        }

        public async Task<IActionResult> Create()
        {
            // Load current user's pets
            var userPets = await _petAppService.GetAllAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 100 });
            ViewBag.UserPets = userPets.Items;
            return View(new CreateAdoptionDto());
        }

        // POST: Create – new adoption listing (with file upload)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAdoptionDto input, IFormFile ImageFile)
        {
            if (!ModelState.IsValid)
            {
                // Re-populate the user pets
                var userPets = await _petAppService.GetAllAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 100 });
                ViewBag.UserPets = userPets.Items;
                return View(input);
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(ms);
                    input.Image = ms.ToArray();
                }
            }
            else
            {
                // Fallback: get the pet's image if available
                var pet = await _petAppService.GetAsync(new EntityDto<int>(input.PetId));
                input.Image = pet.Image;
            }

            // Custom check: Ensure there is an image before proceeding
            if (input.Image == null)
            {
                ModelState.AddModelError("Image", "An image is required either by upload or fallback.");
                var userPets = await _petAppService.GetAllAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 100 });
                ViewBag.UserPets = userPets.Items;
                return View(input);
            }

            await _adoptionAppService.CreateAsync(input);
            return RedirectToAction("MyAdoptions");
        }



        // POST: Edit – update an existing listing
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateAdoptionDto input, IFormFile ImageFile)
        {
            if (!ModelState.IsValid)
            {
                var userPets = await _petAppService.GetAllAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 100 });
                ViewBag.UserPets = userPets.Items;
                return View(input);
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(ms);
                    input.Image = ms.ToArray();
                }
            }
            else
            {
                var existing = await _adoptionAppService.GetAsync(new EntityDto<int>(input.Id));
                input.Image = existing.Image;
            }

            await _adoptionAppService.UpdateAsync(input);
            return RedirectToAction("MyAdoptions");
        }

        // POST: Delete – remove a listing
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _adoptionAppService.DeleteAsync(new EntityDto<int>(id));
            return RedirectToAction("MyAdoptions");
        }
    }
}
