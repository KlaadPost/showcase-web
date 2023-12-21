using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Showcase.Web.Services;

namespace Showcase.Test.ControllerTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Register your mock service here
                services.AddScoped<IRecaptchaService, RecaptchaMockService>();
                services.AddSingleton<IEmailService, EmailMockService>();

                // Call base configuration method to preserve existing configurations
                // (if any) in the application
                base.ConfigureWebHost(builder);
            });
        }
    }
}
