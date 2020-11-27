namespace EShopper.Contracts.V1.Requests.Authentication
{
    public class ResetPasswordRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ResetToken { get; set; }
    }
}