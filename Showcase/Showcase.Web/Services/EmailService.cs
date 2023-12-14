using SendGrid;
using SendGrid.Helpers.Mail;
using Showcase.Web.Models;

namespace Showcase.Web.Services
{
    public class EmailService
    {
        private readonly string _sendGridApiKey;

        public EmailService(string sendGridApiKey)
        {
            _sendGridApiKey = sendGridApiKey;
        }

        public async Task<bool> SendEmail(ContactModel contactData)
        {
            try
            {
                var client = new SendGridClient(_sendGridApiKey);
                var from = new EmailAddress("from@example.com", "Your Name");
                var to = new EmailAddress("to@example.com", "Receiver Name");
                var subject = "New Contact Form Submission";
                var htmlContent = $"<strong>Contact Details:</strong><br>" +
                    $"First Name: {contactData.FirstName}<br>" +
                    $"Last Name: {contactData.LastName}<br>" +
                    $"Email: {contactData.Email}<br>" +
                    $"Phone Number: {contactData.PhoneNumber}";

                var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlContent, null);
                var response = await client.SendEmailAsync(msg);

                return response.StatusCode == System.Net.HttpStatusCode.Accepted;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as required
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
    }
}
