using SendGrid;
using SendGrid.Helpers.Mail;
using Showcase.Web.Models;

namespace Showcase.Web.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _sendGridApiKey;

        public EmailService(string sendGridApiKey)
        {
            _sendGridApiKey = sendGridApiKey;
        }

        public async Task<bool> SendEmail(ContactModel contactData)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress("klaas.post@windesheim.nl", "Showcase");
            var to = new EmailAddress("sprokje@gmail.com", "Klaas");
            var subject = "Nieuw contactverzoek";
            var htmlContent = $"<strong>Contact Details:</strong><br>" +
                $"Voornaam: {contactData.FirstName}<br>" +
                $"Achternaam: {contactData.LastName}<br>" +
                $"Email: {contactData.Email}<br>" +
                $"Telefoonnummer: {contactData.PhoneNumber}";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, "Contact Details", htmlContent);
            var response = await client.SendEmailAsync(msg);

            return response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }
    }
}
