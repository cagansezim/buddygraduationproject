using Abp.Application.Services;
using buddy.VeterinarianProfiles.Dtos;
using System.Threading.Tasks;

namespace buddy.VeterinarianProfiles
{
    public interface IVeterinarianProfileAppService : IApplicationService
    {
        Task<VeterinarianProfileDto> GetOrCreateForUserAsync(long userId);
        Task<VeterinarianProfileDto> GetByUserIdAsync(long userId);
    }
}
