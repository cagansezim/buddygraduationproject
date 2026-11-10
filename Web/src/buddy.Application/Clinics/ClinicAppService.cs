using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using buddy.Clinics.Dtos;
using buddy.Entities.Facilities;
using buddy.Entities.UserProfiles;
using Microsoft.EntityFrameworkCore;

namespace buddy.Clinics
{
    public class ClinicAppService :
        Abp.Application.Services.AsyncCrudAppService<Clinic, ClinicDto, int, PagedAndSortedResultRequestDto>,
        IClinicAppService
    {
        private readonly IRepository<VeterinarianProfile, int> _vetRepo;

        public ClinicAppService(
            IRepository<Clinic, int> clinicRepo,
            IRepository<VeterinarianProfile, int> vetRepo)
            : base(clinicRepo)
        {
            _vetRepo = vetRepo;
        }
        public async Task<PagedResultDto<ClinicWithVeterinarianDto>> GetAllWithVetsAsync()
        {
            var query = Repository.GetAllIncluding();
            var items = ObjectMapper.Map<List<ClinicWithVeterinarianDto>>(query.ToList());
            return new PagedResultDto<ClinicWithVeterinarianDto>(items.Count, items);
        }

        public async Task<ListResultDto<ClinicWithVeterinarianDto>> GetClinicsWithVeterinariansAsync()
        {
            /* clinics */
            var clinics = await Repository.GetAllListAsync();

            /* vets grouped by clinic ( Clinic navigation is mandatory – it has Id ) */
            var vetLookup = await _vetRepo
                .GetAll()
                .Include(v => v.Clinic)
                .Select(v => new { v.Id, ClinicId = v.Clinic.Id, /*v.FirstName, v.LastName*/ })
                .GroupBy(v => v.ClinicId)
                .ToDictionaryAsync(
                    g => g.Key,
                    g => g.Select(x => new VetLiteDto
                    {
                        UserId = x.Id,
                      /*  FullName = $"{x.FirstName} {x.LastName}"*/
                    }).ToList());

            var list = clinics.Select(c =>
            {
                var dto = ObjectMapper.Map<ClinicWithVeterinarianDto>(c);
                dto.Veterinarians = vetLookup.ContainsKey(c.Id)
                    ? vetLookup[c.Id]
                    : new System.Collections.Generic.List<VetLiteDto>();
                return dto;
            }).ToList();

            return new ListResultDto<ClinicWithVeterinarianDto>(list);
        }
    }
}
