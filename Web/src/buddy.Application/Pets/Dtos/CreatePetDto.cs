using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace buddy.Pets.Dtos
{
    public class CreatePetDto : EntityDto<int>
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Species { get; set; }

        [Required]
        [Range(0, 150, ErrorMessage = "Age must be between 0 and 150.")]
        public int Age { get; set; }

        [Required]
        [StringLength(100)]
        public string Sex { get; set; }

        [Range(0, 300)]
        public float Height { get; set; }

        [Range(0, 300)]
        public float Weight { get; set; }

        public bool IsVaccinated { get; set; }

        [Required]
        [StringLength(100)]
        public string Breed { get; set; }

        [DataType(DataType.Upload)]
        public byte[] Image { get; set; }
        public int OwnerId { get; set; }
        
       
    }
}