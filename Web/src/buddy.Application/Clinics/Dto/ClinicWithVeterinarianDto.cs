using System.Collections.Generic;

namespace buddy.Clinics.Dtos
{
    public class ClinicWithVeterinarianDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<VetLiteDto> Veterinarians { get; set; } = new();
    }

    public class VetLiteDto
    {
        public int UserId { get; set; }
        public int? ClinicId { get; set; }
        public string FullName { get; set; }
    }
}
