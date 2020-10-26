namespace EShopper.Contracts.V1.Requests.Authentication
{
    public class RefreshTokenRequestModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}