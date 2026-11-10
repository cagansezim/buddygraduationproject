using AutoMapper;
using buddy.AIDiagnoses.Dtos;
using buddy.Entities.DiseaseandDiagnosis;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace buddy.AIDiagnoses.Mapping
{
    public class AIDiagnosisMapProfile : Profile
    {
        public AIDiagnosisMapProfile()
        {
            /* ⇢ entity ➜ dto (already existed) */
            CreateMap<buddy.Entities.DiseaseandDiagnosis.AIDiagnosis, AIDiagnosisDto>()
                .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreationTime))
                .AfterMap((src, dest) =>
                {
                    if (!string.IsNullOrWhiteSpace(src.Cost_Estimation))
                    {
                        dynamic o = JsonConvert.DeserializeObject(src.Cost_Estimation);
                        dest.Causes = o?.Causes?.ToObject<List<string>>() ?? new List<string>();
                        dest.Recommendations = o?.Recommendations?.ToObject<List<string>>() ?? new List<string>();
                        dest.VetInstructions = o?.VetInstructions?.ToObject<List<string>>() ?? new List<string>();
                    }
                });

            /* ⇢ dto ➜ entity (already existed) */
            CreateMap<AIDiagnosisDto, buddy.Entities.DiseaseandDiagnosis.AIDiagnosis>()
                .ForMember(d => d.Cost_Estimation, opt =>
                    opt.MapFrom(s => JsonConvert.SerializeObject(new
                    {
                        Causes = s.Causes,
                        Recommendations = s.Recommendations,
                        VetInstructions = s.VetInstructions
                    })));

            /* NEW — CreateAIDiagnosisDto ➜ entity  */
            CreateMap<CreateAIDiagnosisDto, buddy.Entities.DiseaseandDiagnosis.AIDiagnosis>()
                .ForMember(d => d.Cost_Estimation, opt =>
                    opt.MapFrom(s => JsonConvert.SerializeObject(new
                    {
                        Causes = s.Causes,
                        Recommendations = s.Recommendations,
                        VetInstructions = s.VetInstructions
                    })));
        }
    }
}
