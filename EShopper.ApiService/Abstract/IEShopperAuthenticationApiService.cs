using EShopper.ApiContracts.V1.Requests.Authentication;
using EShopper.ApiContracts.V1.Responses.Authentication;
using System.Threading.Tasks;

namespace EShopper.ApiService.Abstract
{
    public interface IEShopperAuthenticationApiService : IBaseHttpClient<AuthenticationApiResponseModel>
    {
        Task<AuthenticationApiResponseModel> LoginAsync(LoginRequestApiModel loginRequestApiModel);
    }
}