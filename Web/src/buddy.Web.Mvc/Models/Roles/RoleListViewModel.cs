using System.Collections.Generic;
using buddy.Roles.Dto;

namespace buddy.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
