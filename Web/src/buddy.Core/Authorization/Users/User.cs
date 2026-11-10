using System;
using System.Collections.Generic;
using Abp.Authorization.Users;
using Abp.Extensions;
using buddy.Entities.UserProfiles;

namespace buddy.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public const string DefaultPassword = "123qwe";
        public virtual AdminProfile AdminProfile { get; set; }
        public virtual VeterinarianProfile VeterinarianProfile { get; set; }
        public virtual PetOwnerProfile PetOwnerProfile { get; set; }

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }
    }
}
