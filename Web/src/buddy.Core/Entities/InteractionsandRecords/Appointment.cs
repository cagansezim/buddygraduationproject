using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using buddy.Authorization.Users;
using buddy.Entities.Facilities;
using buddy.Entities.Pets;
using buddy.Entities.UserProfiles;

namespace buddy.Entities.InteractionsandRecords
{
    public class Appointment : Entity<int>
    {
        public int PetId { get; set; } // FK to Pet
        public int PetOwnerUserId { get; set; } // FK to User (replaces PetOwner_Id)
        public int VetUserId { get; set; } // FK to User (replaces Vet_Id)
        public int ClinicId { get; set; } // FK to Clinic
        public int? AppointmentId { get; set; } // FK to itself (for recursion)
        public int? RecordId { get; set; } // FK to Record
        public int? SurveyId { get; set; } // FK to Survey
        public string Status { get; set; }
        public DateTime Created_at { get; set; }

        public virtual Pet Pet { get; set; }
        public virtual PetOwnerProfile PetOwner { get; set; } // Via UserId
        public virtual VeterinarianProfile Veterinarian { get; set; } // Via UserId
        public virtual Clinic Clinic { get; set; }
        public virtual Record Record { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual Appointment ParentAppointment { get; set; } // For recursive relationship
    }
}
