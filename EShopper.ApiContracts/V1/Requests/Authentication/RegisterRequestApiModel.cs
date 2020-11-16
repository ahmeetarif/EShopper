namespace EShopper.ApiContracts.V1.Requests.Authentication
{
    public class RegisterRequestApiModel
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}