using System.Threading.Tasks;

namespace EShopper.EmailManager.Abstract
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MessageContent message);
        Task SendHtmlBodyEmailAsync(MessageContent message);
    }
}