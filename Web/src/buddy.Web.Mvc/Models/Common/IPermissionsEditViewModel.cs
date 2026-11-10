using System.Collections.Generic;
using buddy.Roles.Dto;

namespace buddy.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}