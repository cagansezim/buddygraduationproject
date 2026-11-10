using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using buddy.Entities.UserProfiles;
using System;

namespace buddy.VeterinarianProfiles.Dtos
{
    [AutoMapFrom(typeof(VeterinarianProfile))]
    public class VeterinarianProfileDto : EntityDto<int>
    {
        public long UserId { get; set; }
        public string Qualifications { get; set; }
        public string Specialization { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
