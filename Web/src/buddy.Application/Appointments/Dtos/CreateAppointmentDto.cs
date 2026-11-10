using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace buddy.Appointments.Dtos
{
    public class CreateAppointmentDto : EntityDto<int>
    {
        [Required(ErrorMessage = "Please select a pet.")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid pet selected.")]
        public int PetId { get; set; }

        // Pet owner is assumed to be the current logged-in user.

        [Required(ErrorMessage = "Please select a veterinarian.")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid veterinarian selected.")]
        public int VetUserId { get; set; }

        [Required(ErrorMessage = "Please select a clinic.")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid clinic selected.")]
        public int ClinicId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50)]
        public string Status { get; set; }

        [Required(ErrorMessage = "Please select a scheduled date and time.")]
        public DateTime ScheduledDateTime { get; set; }
    }
}
