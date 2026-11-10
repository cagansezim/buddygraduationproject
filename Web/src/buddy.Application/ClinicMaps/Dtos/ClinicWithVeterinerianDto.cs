using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace buddy.ClinicMaps.Dtos
{
    public class ClinicWithVeterinarianDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        // Optionally include coordinates if available:
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public List<VeterinarianProfileDto> Veterinarians { get; set; }
    }
}
