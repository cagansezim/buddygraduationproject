using Abp.Application.Services;
using Abp.Application.Services.Dto;
using buddy.Adoptions.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace buddy.Adoptions
{
    public interface IAdoptionAppService : IAsyncCrudAppService<AdoptionDto, int, PagedAdoptionResultRequestDto, CreateAdoptionDto, UpdateAdoptionDto>
    {
        Task<ListResultDto<AdoptionDto>> GetUserAdoptions();
    }
}
