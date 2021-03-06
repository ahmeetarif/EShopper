﻿using EShopper.ApiContracts.V1.Requests.Authentication;
using EShopper.ApiContracts.V1.Responses.Authentication;
using EShopper.ApiService.Abstract;
using System.Net.Http;
using System.Threading.Tasks;

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
            var response = await base.PostAsync("v1/auth/login", loginRequestApiModel);
            return response.Result;
        }
    }
}