using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using buddy.Pets.Dtos;

namespace buddy.Pets
{
    public interface IPetAppService : IAsyncCrudAppService<PetDto, int, PagedAndSortedResultRequestDto, CreatePetDto, PetDto>
    {

    }
}
