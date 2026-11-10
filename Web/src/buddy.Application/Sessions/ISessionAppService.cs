using System.Threading.Tasks;
using Abp.Application.Services;
using buddy.Sessions.Dto;

namespace buddy.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
