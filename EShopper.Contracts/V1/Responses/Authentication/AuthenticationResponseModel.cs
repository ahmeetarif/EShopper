using EShopper.Dto.Models;
using System.Runtime.Serialization;

namespace EShopper.Contracts.V1.Responses.Authentication
{
    public class AuthenticationResponseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Message { get; set; }

        public EShopperUserDto EShopperUser { get; set; }
    }
}