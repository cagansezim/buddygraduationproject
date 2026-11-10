using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using buddy.ClinicMaps;
using Abp.Application.Services.Dto;
using buddy.Controllers;

namespace buddy.Web.Controllers
{
    public class ClientClinicMapController : buddyControllerBase
    {
        private readonly IClinicMapAppService _clinicMapAppService;

        public ClientClinicMapController(IClinicMapAppService clinicMapAppService)
        {
            _clinicMapAppService = clinicMapAppService;
        }

        public async Task<IActionResult> Index()
        {
            var clinics = await _clinicMapAppService.GetClinicsWithVeterinariansAsync();
            return View(clinics);
        }
    }
}
