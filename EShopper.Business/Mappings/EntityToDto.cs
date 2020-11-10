using AutoMapper;
using EShopper.Dto.Models;
using EShopper.Entities.Models;

namespace EShopper.Business.Mappings
{
    public class EntityToDto : Profile
    {
        public EntityToDto()
        {
            CreateMap<EShopperUser, EShopperUserDto>();
            CreateMap<UserDetails, UserDetailsDto>();
        }
    }
}