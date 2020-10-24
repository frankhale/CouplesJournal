using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CouplesJournal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(opts =>
                    {
                        opts.ListenAnyIP(5000);
                        opts.ListenAnyIP(5001, opts => opts.UseHttps());
                    });
                });
    }
}
