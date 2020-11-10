using EShopper.ApiContracts.V1.Responses.Authentication;

namespace EShopper.ApiService.Abstract
{
    public interface IEShopperApiService : IBaseHttpClient<AuthenticationApiResponseModel>
    {
    }
}