using CouplesJournal.Blazor.Data;
using CouplesJournal.Blazor.Data.API;
using CouplesJournal.EmailProcessor.Mail;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;


namespace CouplesJournal.EmailProcessor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var userName = Environment.GetEnvironmentVariable("COUPLES_JOURNAL_SMTP_USERNAME");
            var password = Environment.GetEnvironmentVariable("COUPLES_JOURNAL_SMTP_PASSWORD");
            var host = Environment.GetEnvironmentVariable("COUPLES_JOURNAL_SMTP_SERVER");
            var port = Int32.Parse(Environment.GetEnvironmentVariable("COUPLES_JOURNAL_SMTP_PORT"));

            var mailService = new MailService(new MailSettings
            {
                UserName = userName,
                Password = password,
                Host = host,
                Port = port,
                SubjectPrefix = "[Couples Journal] ",
                UseTls = true,
                FromAddress = Environment.GetEnvironmentVariable("COUPLES_JOURNAL_SMTP_USERNAME")
            });

            var api = new CouplesJournalDataApi(new CouplesJournalDbContext(Environment.GetEnvironmentVariable("COUPLES_JOURNAL_CONNECTION_STRING")));
            var emailsToProcess = await api.GetEmailNotificationsToProcess();

            // send the emails here...
        }
    }
}
