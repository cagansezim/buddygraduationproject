using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using buddy.Configuration.Dto;

namespace buddy.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : buddyAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
