using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace buddy.Entities.DiseaseandDiagnosis
{
    public abstract class Diagnosis : FullAuditedEntity<int>
    {
        public int RecordId { get; set; } // FK to Record
    }
}
