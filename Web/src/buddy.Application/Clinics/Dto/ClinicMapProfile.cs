using AutoMapper;
using buddy.Authorization.Users;
using buddy.Entities.Facilities;
using buddy.Entities.UserProfiles;

namespace buddy.Clinics.Dtos
{
    public class ClinicMapProfile : Profile
    {
        public ClinicMapProfile()
        {
            CreateMap<Clinic, ClinicDto>();
            CreateMap<ClinicDto, Clinic>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Appointments, opt => opt.Ignore())
                .ForMember(dest => dest.Records, opt => opt.Ignore())
                .ForMember(dest => dest.ClinicRatings, opt => opt.Ignore());

            CreateMap<(VeterinarianProfile vp, User u), VetLiteDto>()
              .ForMember(d => d.UserId, m => m.MapFrom(s => (int)s.u.Id))
              .ForMember(d => d.ClinicId, m => m.MapFrom(s => s.vp.ClinicId))
              .ForMember(d => d.FullName,
                         m => m.MapFrom(s => $"{s.u.Name} {s.u.Surname}".Trim()));
        }
    }

}
