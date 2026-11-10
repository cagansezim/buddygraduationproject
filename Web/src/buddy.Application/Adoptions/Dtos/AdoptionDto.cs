using System;
using Abp.Application.Services.Dto;

namespace buddy.Adoptions.Dtos
{
    public class AdoptionDto : EntityDto<int>
    {
        // Image data
        public byte[] Image { get; set; }
        public string ImageBase64 { get; set; }

        // Adoption description, listing pet's story
        public string Description { get; set; }

        // Pet information (will be mapped from the Pet entity)
        public int PetId { get; set; }
        public string PetName { get; set; }
        public string Species { get; set; }
        public string Race { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public bool Is_vaccinated { get; set; }

        // Pet owner information
        public int PetOwnerId { get; set; }
        public string PetOwnerName { get; set; }

        // Clinic information (if any)
        public int ClinicId { get; set; }

        // When the listing was created
        public DateTime Created_at { get; set; }
    }
}
