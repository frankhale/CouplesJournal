using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CouplesJournal.Mail
{
    public class MailRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}
