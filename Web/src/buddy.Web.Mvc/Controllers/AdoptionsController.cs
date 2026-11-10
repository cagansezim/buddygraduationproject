using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using buddy.Authorization;
using buddy.Controllers;
using buddy.Adoptions;
using buddy.Adoptions.Dtos;

namespace buddy.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Adoptions)]
    public class AdoptionsController : buddyControllerBase
    {
        private readonly IAdoptionAppService _adoptionAppService;

        public AdoptionsController(IAdoptionAppService adoptionAppService)
        {
            _adoptionAppService = adoptionAppService;
        }
        public async Task<IActionResult> Index()
        {
            //        var adoptions = await _adoptionAppService.GetAllAsync(new PagedAndSortedResultRequestDto());
            //      return View(adoptions.Items);
            return View();
        }

        public async Task<ActionResult> CreateModal()
        {
            var model = new CreateAdoptionDto();
            return PartialView("_CreateModal", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdoptionDto input)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateModal", input);
            }

           // input.PetOwnerId = (int)AbpSession.UserId.Value; // Assign logged-in user
            await _adoptionAppService.CreateAsync(input);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> EditModal(int adoptionId)
        {
            var adoption = await _adoptionAppService.GetAsync(new EntityDto<int>(adoptionId));
            return PartialView("_EditModal", adoption);
        }
    }
}
