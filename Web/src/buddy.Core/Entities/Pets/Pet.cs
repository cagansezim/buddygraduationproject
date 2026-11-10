using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using buddy.Entities.InteractionsandRecords;
using buddy.Entities.UserProfiles;

namespace buddy.Entities.Pets
{
    public class Pet : Entity<int>
    {
        public int UserId { get; set; } // FK to User (replaces Pet_Owner_Id)
        public string Name { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public bool Is_vaccinated { get; set; }
        public string Race { get; set; }
        public byte[] Image { get; set; }
        public DateTime Created_at { get; set; }

        public virtual PetOwnerProfile PetOwner { get; set; }
        public virtual ICollection<Record> Records { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
