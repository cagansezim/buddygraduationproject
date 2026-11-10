using Abp.Application.Services;
using buddy.PetOwnerProfile.Dtos;
using System;
using System.Threading.Tasks;

namespace buddy.PetOwnerProfile
{
    public interface IPetOwnerProfileAppService : IApplicationService
    {
        Task<PetOwnerProfileDto> GetOrCreateForUserAsync(long userId);
        Task<PetOwnerProfileDto> GetByUserIdAsync(long userId);
    }

    
}