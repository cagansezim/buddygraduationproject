using AutoMapper;
using buddy.Entities.UserProfiles;

namespace buddy.VeterinarianProfiles.Dtos
{
    public class VeterinarianProfileMapProfile : Profile
    {
        public VeterinarianProfileMapProfile()
        {
            CreateMap<VeterinarianProfile, VeterinarianProfileDto>();
            CreateMap<VeterinarianProfileDto, VeterinarianProfile>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                // .ForMember(dest => dest.Created_At, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
        }
    }
}
