﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.SqlServer;
using Showcase.Test.Utilities;
using Showcase.Web.Data;
using Showcase.Web.Services;
using System.Data.Common;
using System;


namespace Showcase.Test
{
    // The CustomWebApplicationFactory is needed to setup a mock version of the Web Application for testing
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseTestServer();
            builder.ConfigureServices(services =>
            {
                // Remove the database service added in the base Program
                var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<ShowcaseWebContext>));

                services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);

                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<ShowcaseWebContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                // Register mock implementation of the RecaptchaService 
                services.AddScoped<IRecaptchaService, RecaptchaMockService>();

                // Register mock implementation of the EmailService 
                services.AddSingleton<IContactService, ContactMockService>();

                // Setup Antiforgery variables so cookies and tokens can be extracted
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
