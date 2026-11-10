using AutoMapper;
using buddy.Entities.Adoptions;
using buddy.Adoptions.Dtos;
using System;

namespace buddy.Adoptions.Dtos
{
    public class AdoptionMapProfile : Profile
    {
        public AdoptionMapProfile()
        {
            CreateMap<CreateAdoptionDto, Adoption>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AdoptionId, opt => opt.Ignore())
                .ForMember(dest => dest.PetOwnerId, opt => opt.Ignore())
                .ForMember(dest => dest.ClinicId, opt => opt.Ignore())
                .ForMember(dest => dest.Created_at, opt => opt.Ignore()); // Will be set by service

            CreateMap<Adoption, AdoptionDto>()
                .ForMember(dest => dest.PetName, opt => opt.MapFrom(src => src.Pet != null ? src.Pet.Name : null))
                .ForMember(dest => dest.Species, opt => opt.MapFrom(src => src.Pet != null ? src.Pet.Species : null))
                .ForMember(dest => dest.Race, opt => opt.MapFrom(src => src.Pet != null ? src.Pet.Race : null))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Pet != null ? src.Pet.Age : 0))
                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Pet != null ? src.Pet.Height : 0))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Pet != null ? src.Pet.Weight : 0))
                .ForMember(dest => dest.Is_vaccinated, opt => opt.MapFrom(src => src.Pet != null ? src.Pet.Is_vaccinated : false))
                .ForMember(dest => dest.PetOwnerName, opt => opt.MapFrom(src => src.PetOwner != null && src.PetOwner.User != null ? $"{src.PetOwner.User.Name} {src.PetOwner.User.Surname}" : null))
                .ForMember(dest => dest.ImageBase64, opt => opt.MapFrom(src => src.Image != null ? Convert.ToBase64String(src.Image) : null));

            CreateMap<UpdateAdoptionDto, Adoption>()
                .ForMember(dest => dest.Pet, opt => opt.Ignore())
                .ForMember(dest => dest.PetOwner, opt => opt.Ignore())
                .ForMember(dest => dest.Clinic, opt => opt.Ignore());
            CreateMap<AdoptionDto, UpdateAdoptionDto>();
        }
    }
}
