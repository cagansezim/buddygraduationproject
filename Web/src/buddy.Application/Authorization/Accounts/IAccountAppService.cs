using System.Threading.Tasks;
using Abp.Application.Services;
using buddy.Authorization.Accounts.Dto;

namespace buddy.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
