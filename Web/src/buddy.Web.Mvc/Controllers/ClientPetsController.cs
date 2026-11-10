using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using buddy.Pets;
using buddy.Pets.Dtos;
using buddy.Controllers;
using System.Web.Http.ModelBinding;

namespace buddy.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ClientPetsController : buddyControllerBase
    {
        private readonly IPetAppService _petAppService;

        public ClientPetsController(IPetAppService petAppService)
        {
            _petAppService = petAppService;
        }

        public async Task<IActionResult> Index(PagedAndSortedResultRequestDto input)
        {
            var pets = await _petAppService.GetAllAsync(input);
            return View(pets);
        }

        public async Task<IActionResult> Details(int id)
        {
            var pet = await _petAppService.GetAsync(new EntityDto<int>(id));
            return View(pet);
        }

        public IActionResult Create()
        {
            return View(new CreatePetDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePetDto input, IFormFile ImageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memoryStream);
                    input.Image = memoryStream.ToArray();
                }
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Lütfen bir resim yükleyin.");
                return View(input);
            }

            await _petAppService.CreateAsync(input);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var pet = await _petAppService.GetAsync(new EntityDto<int>(id));
            return View(pet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PetDto input, IFormFile ImageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memoryStream);
                    input.Image = memoryStream.ToArray();
                }
            }
            else
            {
                var existing = await _petAppService.GetAsync(new EntityDto<int>(input.Id));
                input.Image = existing.Image;
            }

            await _petAppService.UpdateAsync(input);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _petAppService.DeleteAsync(new EntityDto<int>(id));
            return RedirectToAction(nameof(Index));
        }
    }
}