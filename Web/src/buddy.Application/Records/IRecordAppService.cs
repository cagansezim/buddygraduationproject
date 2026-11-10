using Abp.Application.Services;
using Abp.Application.Services.Dto;
using buddy.Records.Dtos;

namespace buddy.Records
{
    public interface IRecordAppService : IAsyncCrudAppService<RecordDto, int, PagedAndSortedResultRequestDto, CreateRecordDto, UpdateRecordDto>
    {
    }
}
