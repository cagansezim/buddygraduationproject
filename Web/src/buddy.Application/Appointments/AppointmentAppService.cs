using Abp.Authorization;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using buddy.Appointments.Dtos;
using buddy.Entities.InteractionsandRecords;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace buddy.Appointments
{
    [AbpAuthorize]  // You can fine-tune the permissions as needed
    public class AppointmentAppService : AsyncCrudAppService<Appointment, AppointmentDto, int, PagedResultRequestDto, CreateAppointmentDto, UpdateAppointmentDto>, IAppointmentAppService
    {
        private readonly IRepository<Appointment, int> _appointmentRepository;

        public AppointmentAppService(IRepository<Appointment, int> appointmentRepository)
            : base(appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public override async Task<AppointmentDto> CreateAsync(CreateAppointmentDto input)
        {
            // Optionally, capture the current user's id as the PetOwner
            var currentUserId = AbpSession.UserId;
            if (!currentUserId.HasValue)
            {
                throw new Abp.Authorization.AbpAuthorizationException("User must be logged in to schedule an appointment.");
            }

            // Map the input DTO to the Appointment entity
            var appointment = ObjectMapper.Map<Appointment>(input);
            appointment.PetOwnerUserId = (int)currentUserId.Value;
            appointment.Created_at = DateTime.UtcNow;

            await _appointmentRepository.InsertAsync(appointment);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(appointment);
        }

        // You can override UpdateAsync, DeleteAsync as needed.
    }
}
