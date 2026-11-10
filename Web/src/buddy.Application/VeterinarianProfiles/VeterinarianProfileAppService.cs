using Abp;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using buddy.Authorization.Users;
using buddy.Entities.UserProfiles;
using buddy.VeterinarianProfiles.Dtos;
using System;
using System.Threading.Tasks;

namespace buddy.VeterinarianProfiles
{
    public class VeterinarianProfileAppService : buddyAppServiceBase, IVeterinarianProfileAppService
    {
        private readonly IRepository<VeterinarianProfile, int> _vetProfileRepository;
        private readonly IRepository<User, long> _userRepository;

        public VeterinarianProfileAppService(
            IRepository<VeterinarianProfile, int> vetProfileRepository,
            IRepository<User, long> userRepository)
        {
            _vetProfileRepository = vetProfileRepository;
            _userRepository = userRepository;
        }

        public async Task<VeterinarianProfileDto> GetOrCreateForUserAsync(long userId)
        {
            var profile = await _vetProfileRepository.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile != null)
                return ObjectMapper.Map<VeterinarianProfileDto>(profile);

            profile = new VeterinarianProfile
            {
                UserId = userId,
               // CreatedAt = DateTime.UtcNow,
               // Qualifications = "Not Specified",
                Specialization = "General"
            };

            await _vetProfileRepository.InsertAsync(profile);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<VeterinarianProfileDto>(profile);
        }

        public async Task<VeterinarianProfileDto> GetByUserIdAsync(long userId)
        {
            var profile = await _vetProfileRepository.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null)
                throw new AbpException($"No VeterinarianProfile found for user ID {userId}.");

            return ObjectMapper.Map<VeterinarianProfileDto>(profile);
        }
    }
}
