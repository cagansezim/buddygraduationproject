using Abp.AspNetCore.Mvc.Authorization;
using buddy.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace buddy.Web.Controllers
{
    [Authorize(Roles = "PetOwner")]
    public class ClientPetOwnerController : buddyControllerBase
    {
        public IActionResult Dashboard() => View();
        public IActionResult Adoptions() => RedirectToAction("Index", "ClientAdoptions");
        public IActionResult Settings() => View();
        public IActionResult Information() => View();
        public IActionResult Logout() => RedirectToAction("Login", "Account");
        public IActionResult FillSurvey() => View();
        public IActionResult MyPets() => RedirectToAction("Index","ClientPets");
        public IActionResult ClinicMap() => RedirectToAction("Index","ClientClinicMap");
        public IActionResult Appointments() => View();
        public IActionResult MyAdoptions() => RedirectToAction("MyAdoptions","ClientAdoptions");
        public IActionResult Profile() => View();
        public IActionResult AdoptionProfile() => View();
        public IActionResult WwwMyPet() => RedirectToAction("Index","ClientAIAnalysis");

    }
}
