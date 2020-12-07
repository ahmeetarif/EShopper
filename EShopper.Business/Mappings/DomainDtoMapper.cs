using System;
using AutoMapper;
using EShopper.Dto.Models;
using EShopper.Entities.Models;

namespace EShopper.Business.Mappings
{
    public class DomainDtoMapper : Profile
    {
        public DomainDtoMapper()
        {
            CreateMap<EShopperUser, EShopperUserDto>().ReverseMap();
            CreateMap<UserDetails, UserDetailsDto>().ReverseMap();
        }
    }
}
