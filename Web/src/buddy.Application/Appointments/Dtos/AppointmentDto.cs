using Abp.Application.Services.Dto;
using System;

namespace buddy.Appointments.Dtos
{
    public class AppointmentDto : EntityDto<int>
    {
        public int PetId { get; set; }
        public int PetOwnerUserId { get; set; }
        public int VetUserId { get; set; }
        public int ClinicId { get; set; }
        public string Status { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public DateTime Created_at { get; set; }
    }
}
