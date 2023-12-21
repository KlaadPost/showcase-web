using Showcase.Web.Models;

namespace Showcase.Web.Services
{
    public class EmailMockService : IEmailService
    {
        public Task<bool> SendEmail(ContactModel contactData)
        {
            Console.WriteLine("Sending email...");
            return Task.FromResult(true);
        }
    }
}
