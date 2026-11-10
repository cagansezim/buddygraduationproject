using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using buddy.Authorization.Users;

namespace buddy.Entities.UserProfiles
{
    public class AdminProfile : Entity<int>
    {
        public long UserId { get; set; } // FK to User
        public string Access_Type { get; set; } // Enum or string for access levels

        public virtual User User { get; set; }
    }
}
