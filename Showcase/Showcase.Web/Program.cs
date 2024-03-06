using Showcase.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Showcase.Web.Data;
using Showcase.Web.Models;
using Showcase.Web.Hubs;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.HttpOverrides;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        // Add Database context
        var connectionString = builder.Configuration.GetConnectionString("ShowcaseWebContextConnection") ?? throw new InvalidOperationException("Connection string 'ShowcaseWebContextConnection' not found.");
        builder.Services.AddDbContext<ShowcaseWebContext>(options => options.UseSqlServer(connectionString));

        // Add SignalR
        builder.Services.AddSignalR();

        // Add Identity
        builder.Services.AddDefaultIdentity<ShowcaseUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ShowcaseWebContext>();

        // Add the ContactService 
        builder.Services.AddSingleton<IContactService, ContactService>();

        // Add the EmailSender (used by identity and contactservice)
        builder.Services.AddTransient<IEmailSender, EmailSender>(sp =>
        {
            var sendGridApiKey = builder.Configuration["SENDGRID_SHOWCASE_KEY"];
            return new EmailSender(sendGridApiKey);
        });

        // Add the RecaptchaService 
        builder.Services.AddScoped<IRecaptchaService>(sp =>
        {
            var recaptchaSecretKey = builder.Configuration["RECAPTCHA_SHOWCASE_KEY"];
            return new RecaptchaService(recaptchaSecretKey, sp.GetRequiredService<IHttpClientFactory>().CreateClient());
        });

        // Configure the HttpClient
        builder.Services.AddHttpClient<RecaptchaService>(client =>
        {
            client.BaseAddress = new Uri("https://www.google.com/recaptcha/api/");
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseDefaultFiles();
        
        app.UseStaticFiles();

        app.UseRouting();

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
        });

        app.UseAuthorization();

        app.MapRazorPages();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapHub<ChatHub>("/chatHub");

        app.Run();
    }
}
