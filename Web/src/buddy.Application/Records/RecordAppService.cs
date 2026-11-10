using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using buddy.Records.Dtos;
using buddy.Entities.InteractionsandRecords;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using buddy.Entities.DiseaseandDiagnosis;
using System.Collections.Generic;

namespace buddy.Records
{
    public class RecordAppService : AsyncCrudAppService<Record, RecordDto, int, PagedAndSortedResultRequestDto, CreateRecordDto, UpdateRecordDto>, IRecordAppService
    {
        private readonly IRepository<Record, int> _recordRepository;
        private readonly IRepository<buddy.Entities.DiseaseandDiagnosis.AIDiagnosis, int> _aiDiagnosisRepository;
        private readonly IRepository<VeterinarianDiagnosis, int> _vetDiagnosisRepository;
        private readonly IAbpSession _abpSession;

        public RecordAppService(
            IRepository<Record, int> recordRepository,
            IRepository<buddy.Entities.DiseaseandDiagnosis.AIDiagnosis, int> aiDiagnosisRepository,
            IRepository<VeterinarianDiagnosis, int> vetDiagnosisRepository,
            IAbpSession abpSession)
            : base(recordRepository)
        {
            _recordRepository = recordRepository;
            _aiDiagnosisRepository = aiDiagnosisRepository;
            _vetDiagnosisRepository = vetDiagnosisRepository;
            _abpSession = abpSession;
        }

        public override async Task<RecordDto> CreateAsync(CreateRecordDto input)
        {
            var userId = _abpSession.GetUserId();

            var record = ObjectMapper.Map<Record>(input);
            record.Created_at = DateTime.UtcNow;

            await _recordRepository.InsertAsync(record);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(record);
        }

        public override async Task<RecordDto> UpdateAsync(UpdateRecordDto input)
        {
            var record = await _recordRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, record);

            await CurrentUnitOfWork.SaveChangesAsync();
            return MapToEntityDto(record);
        }

        public override async Task<PagedResultDto<RecordDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
        {
            var query = _recordRepository.GetAll()
                .Include(r => r.AIDiagnosis)
                .Include(r => r.VeterinarianDiagnosis);

            var totalCount = await query.CountAsync();
            var items = await query.ToListAsync();

            return new PagedResultDto<RecordDto>(
                totalCount,
                ObjectMapper.Map<List<RecordDto>>(items)
            );
        }
    }
}
