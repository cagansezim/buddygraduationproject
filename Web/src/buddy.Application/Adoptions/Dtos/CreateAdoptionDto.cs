using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace buddy.Adoptions.Dtos
{
    public class CreateAdoptionDto : EntityDto<int>
    {
        [DataType(DataType.Upload)]
        public byte[] Image { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "PetId must be a positive integer.")]
        public int PetId { get; set; }
    }

}
