using EShopper.ApiDto.Models;

namespace EShopper.ApiContracts.V1.Responses.Authentication
{
    public class AuthenticationApiResponseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Message { get; set; }

        public EShopperUserDto EShopperUser { get; set; }
    }
}