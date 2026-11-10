using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace buddy.Authorization
{
    public class buddyAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Pets, L("Pet"));
            context.CreatePermission(PermissionNames.Pages_Adoptions, L("Adoptions"));
            context.CreatePermission(PermissionNames.ImageClassification, L("ImageClassification"));
            context.CreatePermission(PermissionNames.Analysis, L("Analysis"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, buddyConsts.LocalizationSourceName);
        }
    }
}
