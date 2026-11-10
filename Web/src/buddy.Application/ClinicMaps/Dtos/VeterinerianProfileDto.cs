using Abp.Application.Services.Dto;

namespace buddy.ClinicMaps.Dtos
{
    public class VeterinarianProfileDto : EntityDto<int>
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
    }
}
