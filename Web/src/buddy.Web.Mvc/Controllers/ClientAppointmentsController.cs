using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using buddy.Appointments;
using buddy.Appointments.Dtos;
using buddy.Controllers;
using System.Web.Http.ModelBinding;

namespace buddy.Web.Controllers
{
    public class ClientAppointmentsController : buddyControllerBase
    {
        private readonly IAppointmentAppService _appointmentAppService;

        public ClientAppointmentsController(IAppointmentAppService appointmentAppService)
        {
            _appointmentAppService = appointmentAppService;
        }

        // List appointments (similar to adoptions list)
        public async Task<IActionResult> Index(PagedResultRequestDto input)
        {
            var appointments = await _appointmentAppService.GetAllAsync(input);
            return View(appointments);
        }

        // Display form to create appointment
        public async Task<IActionResult> Create()
        {
            // Optionally, load related data such as available pets, vets, clinics, etc.
            return View(new CreateAppointmentDto());
        }

        // POST: Create appointment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAppointmentDto input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            await _appointmentAppService.CreateAsync(input);
            return RedirectToAction("Index");
        }

        // Display appointment details (and edit/cancel links)
        public async Task<IActionResult> Details(int id)
        {
            var appointment = await _appointmentAppService.GetAsync(new EntityDto<int>(id));
            return View(appointment);
        }

        // You can add Edit and Delete actions similar to the Create action as needed.
    }
}
