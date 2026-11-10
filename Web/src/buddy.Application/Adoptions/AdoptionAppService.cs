using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using buddy.Authorization;
using buddy.Authorization.Users;
using buddy.Entities.Adoptions;
using buddy.Entities.UserProfiles;
using buddy.Adoptions.Dtos;
using System.Collections.Generic;

namespace buddy.Adoptions
{
    [AbpAuthorize(PermissionNames.Pages_Adoptions)]
    public class AdoptionAppService : AsyncCrudAppService<Adoption, AdoptionDto, int, PagedAdoptionResultRequestDto, CreateAdoptionDto, UpdateAdoptionDto>, IAdoptionAppService
    {
        private readonly IRepository<Adoption, int> _adoptionRepository;
        private readonly IRepository<buddy.Entities.UserProfiles.PetOwnerProfile, int> _petOwnerProfileRepository;
        private readonly IRepository<User, long> _userRepository;

        public AdoptionAppService(
            IRepository<Adoption, int> adoptionRepository,
            IRepository<buddy.Entities.UserProfiles.PetOwnerProfile, int> petOwnerProfileRepository,
            IRepository<User, long> userRepository)
            : base(adoptionRepository)
        {
            _adoptionRepository = adoptionRepository;
            _petOwnerProfileRepository = petOwnerProfileRepository;
            _userRepository = userRepository;
        }

        public override async Task<AdoptionDto> CreateAsync(CreateAdoptionDto input)
        {
            CheckCreatePermission();

            var currentUserId = AbpSession.UserId;
            if (!currentUserId.HasValue)
            {
                throw new AbpAuthorizationException("User must be logged in to create an adoption.");
            }

            var petOwnerProfile = await _petOwnerProfileRepository.FirstOrDefaultAsync(p => p.UserId == currentUserId.Value);
            if (petOwnerProfile == null)
            {
                throw new AbpAuthorizationException("Pet owner profile not found for the current user.");
            }

            var adoption = ObjectMapper.Map<Adoption>(input);
            adoption.PetOwnerId = petOwnerProfile.Id;
            adoption.CreatorUserId = currentUserId;
            adoption.ClinicId = 1; // Default value
            adoption.Created_at = DateTime.UtcNow;

            await _adoptionRepository.InsertAsync(adoption);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(adoption);
        }

        public override async Task<AdoptionDto> UpdateAsync(UpdateAdoptionDto input)
        {
            CheckUpdatePermission();

            var adoption = await _adoptionRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, adoption);

            await _adoptionRepository.UpdateAsync(adoption);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(adoption);
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();
            await _adoptionRepository.DeleteAsync(input.Id);
        }

        public async Task<ListResultDto<AdoptionDto>> GetUserAdoptions()
        {
            var currentUserId = AbpSession.UserId;
            if (!currentUserId.HasValue)
            {
                throw new AbpAuthorizationException("User must be logged in to view their adoptions.");
            }

            var petOwnerProfile = await _petOwnerProfileRepository.FirstOrDefaultAsync(p => p.UserId == currentUserId.Value);
            if (petOwnerProfile == null)
            {
                petOwnerProfile = new buddy.Entities.UserProfiles.PetOwnerProfile();
            }

            var adoptions = await _adoptionRepository.GetAll()
                .Include(a => a.Pet)
                .Include(a => a.PetOwner).ThenInclude(p => p.User)
                .Where(a => a.PetOwnerId == petOwnerProfile.Id)
                .ToListAsync();

            return new ListResultDto<AdoptionDto>(ObjectMapper.Map<List<AdoptionDto>>(adoptions));
        }

        protected override IQueryable<Adoption> CreateFilteredQuery(PagedAdoptionResultRequestDto input)
        {
            return _adoptionRepository.GetAllIncluding(a => a.Pet, a => a.PetOwner, a => a.PetOwner.User)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), a =>
                    a.Pet.Name.Contains(input.Keyword) ||
                    a.Pet.Race.Contains(input.Keyword) ||
                    a.Description.Contains(input.Keyword))
                .WhereIf(!string.IsNullOrWhiteSpace(input.Species), a => a.Pet.Species == input.Species)
                .WhereIf(input.MaxAge.HasValue, a => a.Pet.Age <= input.MaxAge.Value)
                .WhereIf(input.MaxWeight.HasValue, a => a.Pet.Weight <= (float)input.MaxWeight.Value);
        }

        protected override IQueryable<Adoption> ApplySorting(IQueryable<Adoption> query, PagedAdoptionResultRequestDto input)
        {
            return query.OrderByDescending(a => a.Created_at);
        }

        protected override async Task<Adoption> GetEntityByIdAsync(int id)
        {
            return await _adoptionRepository.GetAllIncluding(a => a.Pet, a => a.PetOwner, a => a.PetOwner.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
 