using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using buddy.Authorization;
using buddy.Controllers;
using buddy.Pets;
using buddy.Pets.Dtos;

namespace buddy.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Pets)]
    public class PetsController : buddyControllerBase
    {
        private readonly IPetAppService _petAppService;

        public PetsController(IPetAppService petAppService)
        {
            _petAppService = petAppService;
        }

        public async Task<IActionResult> Index()
        {
            var pets = await _petAppService.GetAllAsync(new PagedAndSortedResultRequestDto());
            return View(pets.Items);
        }

        public async Task<ActionResult> CreateModal()
        {
            var model = new CreatePetDto();
            return PartialView("_CreateModal", model);
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePetDto input)
        {
            if (!ModelState.IsValid)
            {
                // Return the same modal partial if invalid — but note
                // that as a full page it may look odd. You might want
                // to do something else for invalid state.
                return PartialView("_CreateModal", input);
            }

            input.OwnerId = (int)AbpSession.UserId.Value;
            await _petAppService.CreateAsync(input);

            // Return a normal redirect to the Index action
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> EditModal(int petId)
        {
            var pet = await _petAppService.GetAsync(new EntityDto<int>(petId));
            return PartialView("_EditModal", pet);
        }
    }
}