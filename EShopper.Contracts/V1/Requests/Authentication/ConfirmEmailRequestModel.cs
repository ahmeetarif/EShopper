namespace EShopper.Contracts.V1.Requests.Authentication
{
    public class ConfirmEmailRequestModel
    {
        public string Email { get; set; }
        public string ConfirmationToken { get; set; }
    }
}