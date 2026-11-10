using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Linq;
using System.Threading.Tasks;
using buddy.Entities.Pets;
using buddy.PetOwnerProfile;
using buddy.Pets.Dtos;
using System.Collections.Generic;

namespace buddy.Pets
{
    public class PetAppService : AsyncCrudAppService<Pet, PetDto, int, PagedAndSortedResultRequestDto, CreatePetDto, PetDto>, IPetAppService
    {
        private readonly IRepository<Pet, int> _petRepository;
        private readonly IPetOwnerProfileAppService _petOwnerProfileAppService;
        private readonly IAbpSession _abpSession;

        public PetAppService(
            IRepository<Pet, int> petRepository,
            IPetOwnerProfileAppService petOwnerProfileAppService,
            IAbpSession abpSession)
            : base(petRepository)
        {
            _petRepository = petRepository;
            _petOwnerProfileAppService = petOwnerProfileAppService;
            _abpSession = abpSession;
        }

        public override async Task<PetDto> CreateAsync(CreatePetDto input)
        {
            var userId = _abpSession.GetUserId();
            var profile = await _petOwnerProfileAppService.GetOrCreateForUserAsync(userId);

            var pet = ObjectMapper.Map<Pet>(input);
            pet.Created_at = DateTime.UtcNow;
            pet.UserId = profile.Id; // Pet.UserId’yi PetOwnerProfile.Id’ye ayarla

            await _petRepository.InsertAsync(pet);
            await CurrentUnitOfWork.SaveChangesAsync();
            return MapToEntityDto(pet);
        }

        public override async Task<PetDto> UpdateAsync(PetDto input)
        {
            var userId = _abpSession.GetUserId();
            var profile = await _petOwnerProfileAppService.GetByUserIdAsync(userId);

            var pet = await _petRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, pet);
            pet.UserId = profile.Id; // Güncellenirken de PetOwnerProfile.Id’yi koru

            await CurrentUnitOfWork.SaveChangesAsync();
            return MapToEntityDto(pet);
        }

        public override async Task<PagedResultDto<PetDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
        {
            var userId = _abpSession.GetUserId();
            var profile = await _petOwnerProfileAppService.GetByUserIdAsync(userId);

            var query = _petRepository.GetAll()
                .Where(p => p.UserId == profile.Id);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);
            var items = await AsyncQueryableExecuter.ToListAsync(query);

            return new PagedResultDto<PetDto>(
                totalCount,
                ObjectMapper.Map<List<PetDto>>(items)
            );
        }
    }
}