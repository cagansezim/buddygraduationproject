using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using buddy.Entities.Pets;

namespace buddy.Entities.InteractionsandRecords
{
    public class Survey : Entity<int>
    {
        public int PetId { get; set; } // FK to Pet
        public string Name { get; set; }
        public string Content { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime Created_at { get; set; }
        public virtual Pet Pet { get; set; }
    }
}
