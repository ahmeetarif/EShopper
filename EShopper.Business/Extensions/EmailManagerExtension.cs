using EShopper.EmailManager;
using EShopper.EmailManager.Abstract;
using System.Threading.Tasks;

namespace EShopper.Business.Extensions
{
    public static class EmailManagerExtension
    {
        public static async Task SendEmailConfirmation(this IEmailSender emailSender, string toEmail, string emailConfirmationLink)
        {
            string messageSubject = "EShopper Email Confirmation";

            string callbackUrl = $"https://localhost:44351/api/v1/auth/confirmEmail?email={toEmail}&confirmationToken={System.Net.WebUtility.UrlEncode(emailConfirmationLink)}";

            string messageBody = string.Format("<h1>Hello Welcome to the EShopper, This is email confirmation</h1><br><p>Click the link to confirm your email<a href=\"{0}\"> Click ME</a></p>", callbackUrl);

            var messageContent = new MessageContent(new string[] { toEmail }, messageSubject, messageBody);

            await emailSender.SendHtmlBodyEmailAsync(messageContent);
        }
    }
}