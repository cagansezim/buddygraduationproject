using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using System;
using System.Threading.Tasks;
using buddy.Entities.UserProfiles;
using Abp;
using buddy.Authorization.Users;
using buddy.PetOwnerProfile.Dtos;
using buddy.PetOwnerProfile;

namespace buddy.PetOwnerProfile;

public class PetOwnerProfileAppService : buddyAppServiceBase, IPetOwnerProfileAppService
{
    private readonly IRepository<buddy.Entities.UserProfiles.PetOwnerProfile, int> _petOwnerProfileRepository;
    private readonly IRepository<User, long> _userRepository;

    public PetOwnerProfileAppService(
        IRepository<buddy.Entities.UserProfiles.PetOwnerProfile, int> petOwnerProfileRepository,
        IRepository<User, long> userRepository)
    {
        _petOwnerProfileRepository = petOwnerProfileRepository;
        _userRepository = userRepository;
    }

    public async Task<PetOwnerProfileDto> GetOrCreateForUserAsync(long userId)
    {
        var profile = await _petOwnerProfileRepository.FirstOrDefaultAsync(p => p.UserId == userId);
        if (profile != null)
        {
            return ObjectMapper.Map<PetOwnerProfileDto>(profile);
        }

        var user = await _userRepository.GetAsync(userId);
        profile = new buddy.Entities.UserProfiles.PetOwnerProfile
        {
            UserId = userId,
            Birth_Date = DateTime.Now.AddYears(-30), // Placeholder
            Age = 30 // Placeholder
        };

        await _petOwnerProfileRepository.InsertAsync(profile);
        await CurrentUnitOfWork.SaveChangesAsync();

        return ObjectMapper.Map<PetOwnerProfileDto>(profile);
    }

    public async Task<PetOwnerProfileDto> GetByUserIdAsync(long userId)
    {
        var profile = await _petOwnerProfileRepository.FirstOrDefaultAsync(p => p.UserId == userId);
        if (profile == null)
        {
            throw new AbpException($"No PetOwnerProfile found for user ID {userId}.");
        }
        return ObjectMapper.Map<PetOwnerProfileDto>(profile);
    }
}