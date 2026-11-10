using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using buddy.ClinicMaps.Dtos;
using System.Collections.Generic;

namespace buddy.ClinicMaps
{
    public interface IClinicMapAppService : IApplicationService
    {
        Task<ListResultDto<ClinicWithVeterinarianDto>> GetClinicsWithVeterinariansAsync();
    }
}
