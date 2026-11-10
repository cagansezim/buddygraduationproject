using AutoMapper;
using buddy.Entities.Pets;
using System;

namespace buddy.Pets.Dtos
{
    public class PetMapProfile : Profile
    {
        public PetMapProfile()
        {
            CreateMap<CreatePetDto, Pet>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Created_at, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<Pet, PetDto>()
                .ForMember(dest => dest.ImageBase64,
                           opt => opt.MapFrom(src => src.Image != null ? Convert.ToBase64String(src.Image) : null));

            CreateMap<PetDto, Pet>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
        }
    }
}