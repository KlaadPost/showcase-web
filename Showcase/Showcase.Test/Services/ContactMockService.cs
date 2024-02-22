using Showcase.Web.Models;

namespace Showcase.Web.Services
{
    /// <summary>
    /// A mock implementation of the Email Service. 
    /// Will always succeed in sending an Email.
    /// </summary>
    public class ContactMockService : IContactService
    {
        public Task SendContactRequest(ContactModel contactData)
        {
            Console.WriteLine("Sending email...");
            return Task.FromResult(true);
        }
    }
}
