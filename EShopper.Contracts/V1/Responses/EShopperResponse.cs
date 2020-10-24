using System.Net;
using System.Runtime.Serialization;

namespace EShopper.Contracts.V1.Responses
{
    [DataContract]
    public class EShopperResponse
    {
        public static EShopperResponse Create(HttpStatusCode statusCode, object result = null, string errorMessage = null)
        {
            return new EShopperResponse(statusCode, result, errorMessage);
        }

        [DataMember]
        public string Version { get { return "1.0"; } }

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ErrorMessage { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public object Result { get; set; }

        public EShopperResponse(HttpStatusCode statusCode, object result = null, string errorMessage = null)
        {
            StatusCode = (int)statusCode;
            Result = result;
            ErrorMessage = errorMessage;
        }
    }
}