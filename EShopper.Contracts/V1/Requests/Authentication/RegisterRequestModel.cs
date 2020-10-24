namespace EShopper.Contracts.V1.Requests.Authentication
{
    public partial class RegisterRequestModel
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}