using Abp.AspNetCore.Mvc.Authorization;
using buddy.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace buddy.Web.Controllers
{
    public class ClientVetController : buddyControllerBase
    {
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Appointments()
        {
            return View();
        }
        public IActionResult Patients()
        {
            return View();
        }
        public IActionResult Customers()
        {
            return View();
        }
        public IActionResult Requests()
        {
            return View();
        }
        public IActionResult Adoptions()
        {
            return View();
        }
        public IActionResult PetProfile()
        {
            return View();
        }
        public IActionResult PetRecord()
        {
            return View();
        }
        public IActionResult VetProfile()
        {
            return View();
        }
        public IActionResult VetRecords()
        {
            return View();
        }
        public IActionResult Clinic()
        {
            return View();
        }
    }
}
