using Blazored.LocalStorage;
using CouplesJournal.Areas.Identity;
using CouplesJournal.Data;
using CouplesJournal.Mail;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouplesJournal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("UserDbConnection")));

            services.AddDbContext<CouplesJournalDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("JournalsDbConnection")));

            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.Configure<MailSettings>(config =>
            {
                config.UserName = Environment.GetEnvironmentVariable("COUPLES_JOURNAL_SMTP_USERNAME");
                config.Password = Environment.GetEnvironmentVariable("COUPLES_JOURNAL_SMTP_PASSWORD");
                config.Host = Environment.GetEnvironmentVariable("COUPLES_JOURNAL_SMTP_SERVER");
                config.Port = Int32.Parse(Environment.GetEnvironmentVariable("COUPLES_JOURNAL_SMTP_PORT"));
                config.SubjectPrefix = "[Couples Journal] ";
                config.UseTls = true;
                config.FromAddress = Environment.GetEnvironmentVariable("COUPLES_JOURNAL_SMTP_USERNAME");
            });
            services.AddSingleton<IMailService, MailService>();

            services.AddBlazoredLocalStorage();
            services.AddRazorPages();
            services.AddServerSideBlazor();            
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddDatabaseDeveloperPageExceptionFilter();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            var journalDbContext = serviceProvider.GetService<CouplesJournalDbContext>();
            var userDbContext = serviceProvider.GetService<ApplicationDbContext>();

            journalDbContext.Database.Migrate();
            userDbContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseForwardedHeaders(new ForwardedHeadersOptions
            //{
            //    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            //});

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            CreateDefaultUsersAndRoles(serviceProvider).Wait();
        }

        private async Task CreateDefaultUsersAndRoles(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var hasAdminRole = await roleManager.RoleExistsAsync("Administrator");
            var hasContributorRole = await roleManager.RoleExistsAsync("Contributor");

            if (!hasAdminRole)
            {
                await roleManager.CreateAsync(new IdentityRole("Administrator"));
            }

            if (!hasContributorRole)
            {
                await roleManager.CreateAsync(new IdentityRole("Contributor"));
            }

            var defaultPassword = Configuration.GetSection("DefaultPassword").Value;
            var users = Configuration.GetSection("DefaultUsers").Get<List<string>>();

            foreach (var user in users)
            {
                if (userManager.Users.FirstOrDefault(x => x.UserName == user) == null)
                {
                    userManager.CreateAsync(new IdentityUser
                    {
                        UserName = user,
                        Email = user,
                        EmailConfirmed = true
                    }, defaultPassword).Wait();

                    var newUser = await userManager.FindByEmailAsync(user);
                    userManager.AddToRoleAsync(newUser, "Contributor").Wait();
                }
            }
        }
    }
}
