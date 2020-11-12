using EShopper.ApiContracts.V1.Requests.Authentication;
using EShopper.ApiContracts.V1.Responses.Authentication;
using EShopper.ApiDto.Models;
using EShopper.ApiService.Abstract;
using System.Net.Http;
using System.Threading.Tasks;
using EShopper.ApiContracts.V1;

namespace EShopper.ApiService.Concrete
{
    public class EShopperAuthenticationApiService : BaseHttpClient<AuthenticationApiResponseModel>, IEShopperAuthenticationApiService
    {
        public EShopperAuthenticationApiService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {

        }

        public async Task<AuthenticationApiResponseModel> LoginAsync(LoginRequestApiModel loginRequestApiModel)
        {
            var response = await base.PostAsync("", loginRequestApiModel);
            return response.Result;
        }
    }
}
