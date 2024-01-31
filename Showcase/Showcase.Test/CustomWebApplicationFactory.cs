using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.SqlServer;
using Showcase.Test.Utilities;
using Showcase.Web.Data;
using Showcase.Web.Services;
using System.Data.Common;


namespace Showcase.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseTestServer();
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<ShowcaseWebContext>));

                services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);

                // Use in-memory SQL Server database
                services.AddDbContext<ShowcaseWebContext>((container, options) =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                // Register mock implementation of the RecaptchaService 
                services.AddScoped<IRecaptchaService, RecaptchaMockService>();

                // Register mock implementation of the EmailService 
                services.AddSingleton<IEmailService, EmailMockService>();

                //Setup Antiforgery so POST, PUT, and DELETE actions can work
                services.AddAntiforgery(t =>
                {
                    t.FormFieldName = AntiForgeryHelper.FormFieldName;
                    t.HeaderName = AntiForgeryHelper.HeaderName;
                    t.Cookie.Name = AntiForgeryHelper.CookieName;
                });
            });
        }
    }
}
