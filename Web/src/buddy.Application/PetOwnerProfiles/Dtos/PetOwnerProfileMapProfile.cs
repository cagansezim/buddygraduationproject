using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using buddy.PetOwnerProfile.Dtos;
using buddy.Entities.UserProfiles;

namespace buddy.PetOwnerProfiles.Dtos
{
    public class PetOwnerProfileMapProfile : Profile
    {
        public PetOwnerProfileMapProfile()
        {
            CreateMap<buddy.Entities.UserProfiles.PetOwnerProfile, PetOwnerProfileDto>();
            CreateMap<PetOwnerProfileDto, buddy.Entities.UserProfiles.PetOwnerProfile>();
        }
    }
}
