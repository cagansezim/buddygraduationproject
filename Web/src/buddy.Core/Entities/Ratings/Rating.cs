using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using buddy.Entities.UserProfiles;

namespace buddy.Entities.Ratings
{
    public class Rating : Entity<int>
    {
        public int PetOwnerUserId { get; set; } // FK to User (replaces PetOwner_Id)
        public float Score { get; set; }
        public string Comment { get; set; }
        public DateTime Created_at { get; set; }

        public virtual PetOwnerProfile PetOwner { get; set; }
    }
}
