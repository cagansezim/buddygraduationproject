using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using buddy.Authorization.Users;
using buddy.Entities.InteractionsandRecords;
using buddy.Entities.Pets;
using buddy.Entities.Ratings;

namespace buddy.Entities.UserProfiles
{
    public class PetOwnerProfile : Entity<int>
    {
        public long UserId { get; set; } // FK to User
        public DateTime Birth_Date { get; set; }
        public int Age { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<ClinicRating> ClinicRatings { get; set; }
        public virtual ICollection<Record> Records { get; set; }
    }
}
