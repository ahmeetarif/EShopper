using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace EShopper.EmailManager
{
    public class MessageContent
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public MessageContent(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => MailboxAddress.Parse(x)));
            Subject = subject;
            Content = content;
        }
    }
}