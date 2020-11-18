namespace EShopper.EmailManager
{
    public class EmailOptions
    {
        public string Sender { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPassword { get; set; }
    }
}