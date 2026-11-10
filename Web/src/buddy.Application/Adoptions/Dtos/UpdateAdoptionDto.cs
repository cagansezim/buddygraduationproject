using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace buddy.Adoptions.Dtos
{
    public class UpdateAdoptionDto : EntityDto<int>
    {
        [StringLength(1000)]
        public string Description { get; set; }

        public byte[] Image { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "PetId must be a positive integer.")]
        public int PetId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "PetOwnerId must be a positive integer.")]
        public int PetOwnerId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "ClinicId must be a non-negative integer.")]
        public int ClinicId { get; set; }
    }
}
