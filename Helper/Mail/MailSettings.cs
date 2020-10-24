using System;

namespace CouplesJournal.Mail
{
    public class MailSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseTls { get; set; }
        public string FromAddress { get; set; }
        public string SubjectPrefix { get; set; }
    }
}
