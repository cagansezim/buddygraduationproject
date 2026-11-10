using AutoMapper;
using buddy.Entities.InteractionsandRecords;
using buddy.Records.Dtos;
using System;

namespace buddy.Records.Dtos
{
    public class RecordMapProfile : Profile
    {
        public RecordMapProfile()
        {
            CreateMap<Record, RecordDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Video, opt => opt.MapFrom(src => src.Video));

            CreateMap<CreateRecordDto, Record>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Created_at, opt => opt.Ignore());

            CreateMap<UpdateRecordDto, Record>()
                .ForMember(dest => dest.Created_at, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.Ignore()) // Prevents overwriting image directly
                .ForMember(dest => dest.Video, opt => opt.Ignore()); // Prevents overwriting video directly
        }
    }
}
