using EShopper.ApiContracts.V1.Responses.Authentication;
using EShopper.ApiService.Abstract;
using System.Net.Http;

namespace EShopper.ApiService.Concrete
{
    public class EShopperApiService : BaseHttpClient<AuthenticationApiResponseModel>, IEShopperApiService
    {
        public EShopperApiService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {

        }

        #region Authentication



        #endregion

    }
}