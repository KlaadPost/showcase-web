using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using Showcase.Web.Models;

namespace Showcase.Web.Services
{
    public class ContactService : IContactService
    {
        private readonly IEmailSender _sender;

        public ContactService(IEmailSender emailSender)
        {
            _sender = emailSender;
        }

        public async Task SendContactRequest(ContactModel contactData)
        {
            var to = "sprokje@gmail.com";
            var subject = "Nieuw contactverzoek";
            var htmlContent = $"<strong>Contact Details:</strong><br>" +
                $"Voornaam: {contactData.FirstName}<br>" +
                $"Achternaam: {contactData.LastName}<br>" +
                $"Email: {contactData.Email}<br>" +
                $"Telefoonnummer: {contactData.PhoneNumber}";

            await _sender.SendEmailAsync(to, subject, htmlContent);
        }
    }
}
