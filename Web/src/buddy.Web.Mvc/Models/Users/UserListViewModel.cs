using System.Collections.Generic;
using buddy.Roles.Dto;

namespace buddy.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
