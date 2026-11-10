using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using buddy.Entities.InteractionsandRecords;
using System;

namespace buddy.Records.Dtos
{
    [AutoMapTo(typeof(Record))]
    public class UpdateRecordDto : EntityDto<int> // Changed to inherit EntityDto<int>
    {
        public int PetId { get; set; }
        public int PetOwnerUserId { get; set; }
        public int VetUserId { get; set; }
        public int ClinicId { get; set; }
        public int? VeterinarianDiagnosisId { get; set; }
        public int? AIDiagnosisId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public DateTime Created_at { get; set; }
    }
}
