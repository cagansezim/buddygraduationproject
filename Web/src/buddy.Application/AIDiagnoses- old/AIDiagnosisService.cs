using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using buddy.AIDiagnoses.Dtos;
using buddy.Entities.DiseaseandDiagnosis;
using buddy.Entities.Ratings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace buddy.AIDiagnoses
{
    public class AIDiagnosisAppService : AsyncCrudAppService<buddy.Entities.DiseaseandDiagnosis.AIDiagnosis, AIDiagnosisDto, int, PagedAndSortedResultRequestDto, CreateAIDiagnosisDto, AIDiagnosisDto>, IAIDiagnosisAppService
    {
        public AIDiagnosisAppService(IRepository<buddy.Entities.DiseaseandDiagnosis.AIDiagnosis, int> repository) : base(repository)
        {
        }
        public async Task<DiagnosisRating> CreateOrGetEmptyRatingAsync(long petOwnerUserId)
        {
            var existing = await Repository.GetDbContext()
                .Set<DiagnosisRating>()
                .FirstOrDefaultAsync(r => r.PetOwnerUserId == petOwnerUserId && r.Score == 0);

            if (existing != null) return existing;

            var rating = new DiagnosisRating
            {
                PetOwnerUserId = (int)petOwnerUserId,
                Score = 0,
                Comment = "auto",
                Created_at = DateTime.UtcNow
            };
            await Repository.GetDbContext().AddAsync(rating);
            await Repository.GetDbContext().SaveChangesAsync();
            return rating;
        }

        public override async Task<AIDiagnosisDto> CreateAsync(CreateAIDiagnosisDto input)
        {
            var aiDiagnosis = ObjectMapper.Map<buddy.Entities.DiseaseandDiagnosis.AIDiagnosis>(input);
            await Repository.InsertAsync(aiDiagnosis);
            await CurrentUnitOfWork.SaveChangesAsync();
            return MapToEntityDto(aiDiagnosis);
        }
    }
}
