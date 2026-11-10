using Abp.Authorization;
using buddy.Authorization.Roles;
using buddy.Authorization.Users;

namespace buddy.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
