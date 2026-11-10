using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace buddy.Pets.Dtos
{
    [AutoMapFrom(typeof(Entities.Pets.Pet))]
    public class PetDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
       
        public float Height { get; set; }
        public float Weight { get; set; }
        public bool IsVaccinated { get; set; }
        public string Breed { get; set; }
        public byte[] Image { get; set; }
        public string ImageBase64 { get; set; }
        public DateTime Created_at { get; set; }
        public int OwnerId { get; set; }
        
    }
}