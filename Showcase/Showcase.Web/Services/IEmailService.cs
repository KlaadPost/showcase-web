using Showcase.Web.Models;

namespace Showcase.Web.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(ContactModel contactData);
    }
}
