using AutoMapper;
using EShopper.Contracts.V1.Requests.Category;
using EShopper.Entities.Models;

namespace EShopper.Business.Mappings
{
    public class RequestDomainMapper : Profile
    {
        public RequestDomainMapper()
        {
            CreateMap<CreateCategoryRequestModel, Category>();
        }
    }
}