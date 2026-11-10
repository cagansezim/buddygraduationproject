using System.Collections.Generic;
using buddy.Entities.InteractionsandRecords;
using buddy.Entities.Ratings;
using buddy.Entities.UserProfiles;

namespace buddy.Entities.DiseaseandDiagnosis
{
    public class VeterinarianDiagnosis : Diagnosis
    {
        public int RecordId { get; set; }   // FK ➜ Record
        public int VetUserId { get; set; }   // FK ➜ VeterinarianProfile
        public string TreatmentPlan { get; set; }

        public virtual Record Record { get; set; }
        public virtual VeterinarianProfile Veterinarian { get; set; }

        public virtual ICollection<DiagnosisRating> DiagnosisRatings { get; set; }
                     = new List<DiagnosisRating>();
    }
}
