﻿using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CouplesJournal.Blazor.Areas.Identity.IdentityHostingStartup))]
namespace CouplesJournal.Blazor.Areas.Identity
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