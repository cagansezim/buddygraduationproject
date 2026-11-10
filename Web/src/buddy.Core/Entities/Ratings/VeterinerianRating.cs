using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using buddy.Entities.UserProfiles;

namespace buddy.Entities.Ratings
{
    public class VeterinarianRating : Rating
    {
        public int VetUserId { get; set; } // FK to AbpUsers (for ABP’s user table, representing Veterinarian)

        public virtual PetOwnerProfile PetOwner { get; set; } // Assuming PetOwner rates Veterinarian
        public virtual VeterinarianProfile VeterinarianProfile { get; set; }
    }
}
