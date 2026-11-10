using System.Threading.Tasks;
using buddy.Configuration.Dto;

namespace buddy.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
