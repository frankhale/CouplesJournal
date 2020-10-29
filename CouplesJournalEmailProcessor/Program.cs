using CouplesJournal.Blazor.Data;
using CouplesJournal.Blazor.Data.API;
using CouplesJournal.EmailProcessor.Mail;
using System;
using System.Linq;
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
            var connString = Environment.GetEnvironmentVariable("COUPLES_JOURNAL_CONNECTION_STRING");
            int port = 0;

            try
            {
                port = Int32.Parse(Environment.GetEnvironmentVariable("COUPLES_JOURNAL_SMTP_PORT"));
            }
            catch
            {
                Console.WriteLine("Please check the environment variable COUPLES_JOURNAL_SMTP_PORT as this value could not be converted to an integer.");
            }                      

            if(string.IsNullOrEmpty(userName) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(host) ||                
                string.IsNullOrEmpty(connString))
            {
                Console.WriteLine("The following environment variables need to be set:\n\nCOUPLES_JOURNAL_SMTP_USERNAME\nCOUPLES_JOURNAL_SMTP_PASSWORD\nCOUPLES_JOURNAL_SMTP_SERVER\nCOUPLES_JOURNAL_CONNECTION_STRING");
                Environment.Exit(-1);
            }

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

            var api = new CouplesJournalDataApi(new CouplesJournalDbContext(connString));
            var emailNotifications = await api.GetEmailNotificationsToProcess();
            var emailNotificationsToProcess = emailNotifications.Select(x =>
                mailService.SendEmailAsync(new MailRequest
                {
                    To = x.To,
                    Subject = x.Subject,
                    Body = x.Body
                }));
            var emailNotificationsToSetAsProcessed = emailNotifications.Select(x => api.SetEmailNotificationProcessed(x));

            await Task.WhenAll(emailNotificationsToProcess);
            await Task.WhenAll(emailNotificationsToSetAsProcessed);

            if (emailNotifications.Count() > 0)
            {
                Console.WriteLine($"Processed {emailNotifications.Count()} email notifications...");
            }
            else
            {
                Console.WriteLine("There are no email notifications to process at this time...");
            }
        }
    }
}
