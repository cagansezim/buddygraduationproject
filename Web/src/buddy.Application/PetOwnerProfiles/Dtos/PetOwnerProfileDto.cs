using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace buddy.PetOwnerProfile.Dtos
{
    public class PetOwnerProfileDto : EntityDto<int>
    {
        public long UserId { get; set; }
        public DateTime Birth_Date { get; set; }
        public int Age { get; set; }
    }
}
