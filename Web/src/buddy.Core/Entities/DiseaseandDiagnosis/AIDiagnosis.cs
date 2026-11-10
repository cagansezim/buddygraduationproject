using System.Collections.Generic;
using buddy.Entities.InteractionsandRecords;
using buddy.Entities.Ratings;

namespace buddy.Entities.DiseaseandDiagnosis
{
    public class AIDiagnosis : Diagnosis
    {
        public int RecordId { get; set; }        // FK ➜ Record
        public string Cost_Estimation { get; set; }

        public virtual Record Record { get; set; }

        /* one AI‑Diagnosis ⇢ many ratings */
        public virtual ICollection<DiagnosisRating> DiagnosisRatings { get; set; }
                     = new List<DiagnosisRating>();
    }
}
