using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using buddy.Appointments.Dtos;

namespace buddy.Appointments
{
    public interface IAppointmentAppService : IAsyncCrudAppService<AppointmentDto, int, PagedResultRequestDto, CreateAppointmentDto, UpdateAppointmentDto>
    {
        // Add additional custom appointment-specific methods here if necessary.
    }
}
