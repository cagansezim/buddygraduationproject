using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using buddy.Entities.Facilities;

namespace buddy.Clinics.Dtos
{
    [AutoMapFrom(typeof(Clinic))]
    public class ClinicDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public float Average_Rate { get; set; }
    }
}
