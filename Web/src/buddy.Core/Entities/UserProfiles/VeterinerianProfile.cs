using System;
using System.Collections.Generic;
using Abp.Domain.Entities;
using buddy.Authorization.Users;
using buddy.Entities.DiseaseandDiagnosis;
using buddy.Entities.Facilities;
using buddy.Entities.InteractionsandRecords;

namespace buddy.Entities.UserProfiles
{
    public class VeterinarianProfile : Entity<int>
    {
        /* ───────── Core columns ───────────────────── */
        public long UserId { get; set; }              // FK → User
        public string Access_Type { get; set; }              // e.g. “Owner”, “Staff”
        public string Specialization { get; set; }              // optional text

        /* ───────── Optional link to Clinic ─────────── */
        public int? ClinicId { get; set; }              // FK → Clinic (nullable)

        /* ───────── Navigation properties ───────────── */
        public virtual User User { get; set; }
        public virtual Clinic Clinic { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
                            = new List<Appointment>();

        public virtual ICollection<Record> Records { get; set; }
                            = new List<Record>();

        public virtual ICollection<VeterinarianDiagnosis> VeterinarianDiagnosis { get; set; }
                            = new List<VeterinarianDiagnosis>();
    }
}
