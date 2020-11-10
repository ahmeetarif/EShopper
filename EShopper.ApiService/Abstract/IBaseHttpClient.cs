using EShopper.ApiContracts.V1;
using System.Threading.Tasks;

namespace EShopper.ApiService.Abstract
{
    public interface IBaseHttpClient<T>
        where T : class, new()
    {
        Task<EShopperApiResponse<T>> PostAsync(string requestUri, object dto);
        Task<EShopperApiResponse<T>> GetAsync(string requestUri);
    }
}