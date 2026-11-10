using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using buddy.AIDiagnoses.Dtos;
using buddy.Entities.Ratings;

namespace buddy.AIDiagnoses
{
    public interface IAIDiagnosisAppService : IAsyncCrudAppService<AIDiagnosisDto, int, PagedAndSortedResultRequestDto, CreateAIDiagnosisDto, AIDiagnosisDto>
    {
        Task<DiagnosisRating> CreateOrGetEmptyRatingAsync(long petOwnerUserId);
    }
}
