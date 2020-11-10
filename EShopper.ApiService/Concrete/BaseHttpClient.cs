﻿using EShopper.ApiContracts.V1;
using EShopper.ApiService.Abstract;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.ApiService.Concrete
{
    public class BaseHttpClient<T> : IBaseHttpClient<T>
        where T : class, new()
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseHttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public HttpClient GetClient()
        {
            var client = _httpClientFactory.CreateClient("eshopperApi");
            // TODO: Create request headers
            return client;
        }

        public async Task<EShopperApiResponse<T>> PostAsync(string requestUri, object dto)
        {
            var client = GetClient();
            var jsonRequestData = JsonConvert.SerializeObject(dto);
            var stringContent = new StringContent(jsonRequestData, UnicodeEncoding.UTF8, "application/json");

            var result = await client.PostAsync(requestUri, stringContent);
            var stringResult = await result.Content.ReadAsStringAsync();

            var jsonResponseResult = JsonConvert.DeserializeObject<EShopperApiResponse<T>>(stringResult);

            return jsonResponseResult;
        }

        public async Task<EShopperApiResponse<T>> GetAsync(string requestUri)
        {
            var client = GetClient();
            var apiRequestResponse = await client.GetAsync(requestUri);
            var stringResult = await apiRequestResponse.Content.ReadAsStringAsync();

            var jsonResult = JsonConvert.DeserializeObject<EShopperApiResponse<T>>(stringResult);
            return jsonResult;
        }
    }
}