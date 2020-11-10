using System.Net;
using System.Runtime.Serialization;

namespace EShopper.ApiContracts.V1
{
    public class EShopperApiResponse<T>
        where T : class, new()
    {
        public static EShopperApiResponse<T> Create(HttpStatusCode statusCode, T result = null, string errorMessage = null)
        {
            return new EShopperApiResponse<T>(statusCode, result, errorMessage);
        }

        [DataMember]
        public string Version { get { return "1.0"; } }

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ErrorMessage { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public T Result { get; set; }

        public EShopperApiResponse(HttpStatusCode statusCode, T result = null, string errorMessage = null)
        {
            StatusCode = (int)statusCode;
            Result = result;
            ErrorMessage = errorMessage;
        }
    }
}