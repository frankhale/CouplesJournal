using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.IO;
using System.Threading.Tasks;

namespace CouplesJournal.Mail
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.FromAddress)
            };
            email.From.Add(MailboxAddress.Parse(_mailSettings.FromAddress));
            email.To.Add(MailboxAddress.Parse(mailRequest.To));
            email.Subject = _mailSettings.SubjectPrefix + mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();

            if (_mailSettings.UseTls)
            {
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            }
            else
            {
                // When using Papercut SMTP you don't need authentication or TLS
                smtp.Connect(_mailSettings.Host, _mailSettings.Port);
            }

            if (!string.IsNullOrEmpty(_mailSettings.UserName) && !string.IsNullOrEmpty(_mailSettings.Password))
            {
                smtp.Authenticate(_mailSettings.UserName, _mailSettings.Password);
            }

            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
