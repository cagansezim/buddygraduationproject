using System.Collections.Generic;
using buddy.Entities.InteractionsandRecords;
using buddy.Entities.Ratings;
using buddy.Entities.UserProfiles;

namespace buddy.Entities.Facilities
{
    public class Clinic : Facility
    {
        /* ───────── Clinic details ──────────────────── */
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public float Average_Rate { get; set; }

        /* ───────── Navigation collections ──────────── */
        public virtual ICollection<Appointment> Appointments { get; set; }
                          = new List<Appointment>();

        public virtual ICollection<ClinicRating> ClinicRatings { get; set; }
                          = new List<ClinicRating>();

        public virtual ICollection<Record> Records { get; set; }
                          = new List<Record>();

        /* NEW — back‑reference to vets working here */
        public virtual ICollection<VeterinarianProfile> Veterinarians { get; set; }
                          = new List<VeterinarianProfile>();
    }
}
