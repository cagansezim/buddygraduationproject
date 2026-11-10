using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using buddy.Entities.InteractionsandRecords;
using System;

namespace buddy.Records.Dtos
{
    [AutoMapFrom(typeof(Record))]
    public class RecordDto : EntityDto<int>
    {
        public int PetId { get; set; }
        public int PetOwnerUserId { get; set; }
        public int VetUserId { get; set; }
        public int ClinicId { get; set; }
        public int? VeterinarianDiagnosisId { get; set; }
        public int? AIDiagnosisId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Raw data
        public byte[] Image { get; set; }
        public byte[] Video { get; set; }

        // Base64 for front-end
        public string ImageBase64 => Image != null ? Convert.ToBase64String(Image) : null;
        public string VideoBase64 => Video != null ? Convert.ToBase64String(Video) : null;

        public DateTime Created_at { get; set; }
    }
}
