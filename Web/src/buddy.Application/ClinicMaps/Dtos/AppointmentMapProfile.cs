using AutoMapper;
using buddy.Entities.Facilities;
using buddy.ClinicMaps.Dtos;
using buddy.Entities.UserProfiles; // For VeterinarianProfile

namespace buddy
{
    public class AppointmentMapProfile : Profile
    {
        public AppointmentMapProfile()
        {
            // Mapping Clinic to ClinicWithVeterinarianDto
            CreateMap<Clinic, ClinicWithVeterinarianDto>();

            // Mapping VeterinarianProfile to VeterinarianProfileDto.
            CreateMap<VeterinarianProfile, VeterinarianProfileDto>()
                // Optionally map a full name if your VeterinarianProfile has a User property.
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
}
