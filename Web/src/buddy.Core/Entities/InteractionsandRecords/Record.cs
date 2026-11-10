using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using buddy.Authorization.Users;
using buddy.Entities.DiseaseandDiagnosis;
using buddy.Entities.Facilities;
using buddy.Entities.Pets;
using buddy.Entities.UserProfiles;

namespace buddy.Entities.InteractionsandRecords
{
    public class Record : Entity<int>
    {
        public int PetId { get; set; } // FK to Pet
        public int PetOwnerUserId { get; set; } // FK to User (replaces PetOwner_Id)
        public int VetUserId { get; set; } // FK to User (replaces Vet_Id)
        public int ClinicId { get; set; } // FK to Clinic
        public int? VeterinarianDiagnosisId { get; set; } // FK to VetDiagnosis
        public int? AIDiagnosisId { get; set; } // FK to AIDiagnosis
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public DateTime Created_at { get; set; }

        public virtual Pet Pet { get; set; }
        public virtual PetOwnerProfile PetOwner { get; set; } // Via UserId
        public virtual VeterinarianProfile Veterinarian { get; set; } // Via UserId
        public virtual Clinic Clinic { get; set; }
        public virtual VeterinarianDiagnosis VeterinarianDiagnosis { get; set; }
        public virtual AIDiagnosis AIDiagnosis { get; set; }
    }
}
