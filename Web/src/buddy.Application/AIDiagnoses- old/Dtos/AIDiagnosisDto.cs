using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using buddy.Entities.DiseaseandDiagnosis;
using System;
using System.Collections.Generic;

namespace buddy.AIDiagnoses.Dtos
{
    [AutoMapFrom(typeof(buddy.Entities.DiseaseandDiagnosis.AIDiagnosis))]
    public class AIDiagnosisDto : EntityDto<int>
    {
        public int RecordId { get; set; }
        public int DiagnosisRatingId { get; set; }
        public string Cost_Estimation { get; set; }
        public DateTime CreatedAt { get; set; }

        // Newly added properties for JSON data
        public List<string> Causes { get; set; } = new List<string>();
        public List<string> Recommendations { get; set; } = new List<string>();
        public List<string> VetInstructions { get; set; } = new List<string>();
    }
}
