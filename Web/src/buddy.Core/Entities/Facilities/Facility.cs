using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace buddy.Entities.Facilities
{
    public abstract class Facility : FullAuditedEntity<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public float Average_Rate { get; set; }
    }
}
