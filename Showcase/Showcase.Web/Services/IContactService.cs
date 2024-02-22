using Showcase.Web.Models;

namespace Showcase.Web.Services
{
    public interface IContactService
    {
        Task SendContactRequest(ContactModel contactData);
    }
}
