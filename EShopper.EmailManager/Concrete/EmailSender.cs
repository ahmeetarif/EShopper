using EShopper.EmailManager.Abstract;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace EShopper.EmailManager.Concrete
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailOptions _emailOptions;
        public EmailSender(EmailOptions emailOptions)
        {
            _emailOptions = emailOptions;
        }

        public async Task SendEmailAsync(MessageContent message)
        {
            var emailMessage = CreateMailMessage(message);
            await SendAsync(emailMessage);
        }

        public async Task SendHtmlBodyEmailAsync(MessageContent message)
        {
            var emailMessage = CreateHtmlMailMessage(message);
            await SendAsync(emailMessage);
        }

        #region Private Functions

        private MimeMessage CreateMailMessage(MessageContent message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse(_emailOptions.Sender));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message.Content
            };

            return emailMessage;
        }

        private MimeMessage CreateHtmlMailMessage(MessageContent message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse(_emailOptions.Sender));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message.Content
            };

            return emailMessage;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailOptions.SmtpServer, _emailOptions.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailOptions.SenderEmail, _emailOptions.SenderPassword);

                    await client.SendAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    // TODO: Log Exception
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }

        #endregion
    }
}