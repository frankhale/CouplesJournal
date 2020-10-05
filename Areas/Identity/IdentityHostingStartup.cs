using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CouplesJournal.Areas.Identity.IdentityHostingStartup))]
namespace CouplesJournal.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}