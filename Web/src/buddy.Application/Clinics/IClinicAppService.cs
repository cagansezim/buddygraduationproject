using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using buddy.Clinics.Dtos;

namespace buddy.Clinics
{
    public interface IClinicAppService :
        IAsyncCrudAppService<ClinicDto, int, PagedAndSortedResultRequestDto>
    {
        Task<ListResultDto<ClinicWithVeterinarianDto>> GetClinicsWithVeterinariansAsync();
        Task<PagedResultDto<ClinicWithVeterinarianDto>> GetAllWithVetsAsync();

    }
}
