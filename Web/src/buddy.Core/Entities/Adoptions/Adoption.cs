using Abp.Domain.Entities.Auditing;
using buddy.Entities.Facilities;
using buddy.Entities.Pets;
using buddy.Entities.UserProfiles;
using System;

namespace buddy.Entities.Adoptions
{
    public class Adoption : FullAuditedEntity<int>
    {
        public int AdoptionId { get; set; }
        public DateTime Created_at { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }        
        public int PetId { get; set; } 
        public int PetOwnerId { get; set; } 
        public int ClinicId { get; set; }         
        public virtual Pet Pet { get; set; }
        public virtual PetOwnerProfile PetOwner { get; set; }
        public virtual Clinic Clinic { get; set; }
    }
}
