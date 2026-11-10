using System.Collections.Generic;
using Abp.AutoMapper;
using buddy.Entities.DiseaseandDiagnosis;

namespace buddy.AIDiagnoses.Dtos
{
    /*
     * One DTO — still mapped to the single JSON column ‘Cost_Estimation’.
     */
    [AutoMapTo(typeof(buddy.Entities.DiseaseandDiagnosis.AIDiagnosis))]
    public class CreateAIDiagnosisDto
    {
        public int RecordId { get; set; }
        public int DiagnosisRatingId { get; set; }

        public List<string> Causes { get; set; } = new();
        public List<string> Recommendations { get; set; } = new();
        public List<string> VetInstructions { get; set; } = new();

        // stays in the class so the DB column is filled by the mapper
        public string Cost_Estimation { get; set; }
    }
}
