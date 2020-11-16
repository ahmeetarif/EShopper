using AutoMapper;
using EShopper.ApiContracts.V1.Requests.Authentication;
using EShopper.UI.Models.Authentication;

namespace EShopper.UI.Mappings
{
    public class ViewModelToApiRequestModel : Profile
    {
        public ViewModelToApiRequestModel()
        {
            CreateMap<LoginViewModel, LoginRequestApiModel>();
        }
    }
}