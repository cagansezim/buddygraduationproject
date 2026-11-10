using buddy.Entities.DiseaseandDiagnosis;

namespace buddy.Entities.Ratings
{
    public class DiagnosisRating : Rating
    {
        public int? VeterinarianDiagnosisId { get; set; }   // optional
        public int? AIDiagnosisId { get; set; }   // optional
        public float AccuracyScore { get; set; }

        public virtual VeterinarianDiagnosis VeterinarianDiagnosis { get; set; }
        public virtual AIDiagnosis AIDiagnosis { get; set; }
    }
}
