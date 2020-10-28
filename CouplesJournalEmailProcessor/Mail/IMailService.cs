using System.Threading.Tasks;

namespace CouplesJournal.EmailProcessor.Mail
{
    // TUTURIAL: https://www.codewithmukesh.com/blog/send-emails-with-aspnet-core/
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
