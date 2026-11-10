using Abp.Application.Services;
using Abp.Application.Services.Dto;
using buddy.ClinicMaps.Dtos;
using buddy.Entities.Facilities;       // Assuming Clinic entity is here.
using buddy.Entities.InteractionsandRecords; // For Appointment entity.
using buddy.Entities.UserProfiles;       // For VeterinarianProfile entity.
using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace buddy.ClinicMaps
{
    public class ClinicMapAppService : ApplicationService, IClinicMapAppService
    {
        private readonly IRepository<Clinic, int> _clinicRepository;
        private readonly IRepository<Appointment, int> _appointmentRepository;
        private readonly IRepository<VeterinarianProfile, int> _veterinarianProfileRepository;

        public ClinicMapAppService(IRepository<Clinic, int> clinicRepository,
                                   IRepository<Appointment, int> appointmentRepository,
                                   IRepository<VeterinarianProfile, int> veterinarianProfileRepository)
        {
            _clinicRepository = clinicRepository;
            _appointmentRepository = appointmentRepository;
            _veterinarianProfileRepository = veterinarianProfileRepository;
        }

        public async Task<ListResultDto<ClinicWithVeterinarianDto>> GetClinicsWithVeterinariansAsync()
        {
            // Retrieve all clinics.
            var clinics = await _clinicRepository.GetAllListAsync();
            var clinicDtos = new List<ClinicWithVeterinarianDto>();

            foreach (var clinic in clinics)
            {
                // Map the clinic to the DTO.
                var clinicDto = ObjectMapper.Map<ClinicWithVeterinarianDto>(clinic);

                // Find distinct veterinarian user IDs from appointments for this clinic.
                var vetUserIds = await _appointmentRepository
                    .GetAll()
                    .Where(a => a.ClinicId == clinic.Id)
                    .Select(a => a.VetUserId)
                    .Distinct()
                    .ToListAsync();

                // Load veterinarian profiles where the underlying UserId matches one of these values.
                var veterinarians = await _veterinarianProfileRepository
                    .GetAll()
                    .Where(v => vetUserIds.Contains((int)v.UserId))
                    .ToListAsync();

                // Map to DTOs.
                clinicDto.Veterinarians = ObjectMapper.Map<List<VeterinarianProfileDto>>(veterinarians);
                clinicDtos.Add(clinicDto);
            }

            return new ListResultDto<ClinicWithVeterinarianDto>(clinicDtos);
        }
    }
}
