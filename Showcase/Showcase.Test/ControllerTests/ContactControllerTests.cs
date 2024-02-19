using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Showcase.Test.Utilities;
using Showcase.Web.Models;
using System.Net;
using System.Text;
using Xunit;

namespace Showcase.Test.ControllerTests
{
    public class ContactControllerTests : IDisposable
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public ContactControllerTests() 
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true
            });
        }

        [Fact]
        public async Task ContactActionReturnsView()
        {
            var response = await _client.GetAsync("/Contact");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ContactPostCorrectReturnsSuccess()
        {
            // Arrange
            ContactModel contactModel = new()
            {
                FirstName = "Jaap", // Valid
                LastName = "Saus", // Valid
                Email = "jaap@saus.nl", // Valid
                PhoneNumber = "0600000002", // Valid
                RecaptchaToken = "1234567890" // Mock token representing a valid one
            };

            StringContent stringContent = new(JsonConvert.SerializeObject(contactModel), Encoding.UTF8, "application/json");

            // Act
            var response = await RequestHelper.PostAsyncWithHeaders(_client, "/Contact", stringContent);

            string responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Contact request has been sent", responseContent);
        }

        [Fact]
        public async Task ContactPostSuspicioosCaptchaReturnsError()
        {
            // Arrange
            ContactModel contactModel = new()
            {
                FirstName = "Jaap", // Valid
                LastName = "Saus", // Valid
                Email = "jaap@saus.nl", // Valid
                PhoneNumber = "0600000002", // Valid
                RecaptchaToken = "13249831980" // Mock token representing a suspicious one
            };

            StringContent stringContent = new(JsonConvert.SerializeObject(contactModel), Encoding.UTF8, "application/json");

            // Act
            var response = await RequestHelper.PostAsyncWithHeaders(_client, "/Contact", stringContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(responseContent);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Your request has been flagged as suspicious, please try again later", errorResponse["RecaptchaToken"]);
        }

        [Fact]
        public async Task ContactPostNoInputReturnsError()
        {
            // Arrange
            ContactModel contactModel = new()
            {
                FirstName = "",
                LastName = "",
                Email = "",
                PhoneNumber = "",
                RecaptchaToken = ""
            };

            StringContent stringContent = new(JsonConvert.SerializeObject(contactModel), Encoding.UTF8, "application/json");

            // Act
            var response = await RequestHelper.PostAsyncWithHeaders(_client, "/Contact", stringContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(responseContent);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("First name is required", errorResponse["FirstName"]);
            Assert.Contains("Last name is required", errorResponse["LastName"]);
            Assert.Contains("A Recaptcha token is required", errorResponse["RecaptchaToken"]);
        }

        [Fact]
        public async Task ContactPostMaxLengthExceededReturnsError()
        {
            // Arrange
            ContactModel contactModel = new()
            {
                FirstName = new string('a', 256),
                LastName = new string('b', 256),
                Email = new string('c', 256) + "@example.com",
                PhoneNumber = "0600000002", // Valid
                RecaptchaToken = "1234567890" // Mock token representing a valid one
            };

            StringContent stringContent = new(JsonConvert.SerializeObject(contactModel), Encoding.UTF8, "application/json");

            // Act
            var response = await RequestHelper.PostAsyncWithHeaders(_client, "/Contact", stringContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(responseContent);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("First name exceeds maximum length of 255 characters", errorResponse["FirstName"]);
            Assert.Contains("Last name exceeds maximum length of 255 characters", errorResponse["LastName"]);
            Assert.Contains("Email exceeds maximum length of 255 characters", errorResponse["Email"]);
        }

        [Fact]
        public async Task ContactPostInvalidEmailReturnsError()
        {
            // Arrange
            ContactModel contactModel = new()
            {
                FirstName = "Jaap", // Valid
                LastName = "Saus", // Valid
                Email = "invalid_email",
                PhoneNumber = "0600000002", // Valid
                RecaptchaToken = "1234567890" // Mock token representing a valid one
            };

            StringContent stringContent = new(JsonConvert.SerializeObject(contactModel), Encoding.UTF8, "application/json");

            // Act
            var response = await RequestHelper.PostAsyncWithHeaders(_client, "/Contact", stringContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(responseContent);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Please provide a valid email address", errorResponse["Email"]);
        }

        [Fact]
        public async Task ContactPostInvalidPhoneNumberReturnsError()
        {
            // Arrange
            ContactModel contactModel = new()
            {
                FirstName = "Jaap", // Valid
                LastName = "Saus", // Valid
                Email = "jaap@saus.nl", // Valid
                PhoneNumber = "invalid_phone",
                RecaptchaToken = "1234567890" // Mock token representing a valid one
            };

            StringContent stringContent = new(JsonConvert.SerializeObject(contactModel), Encoding.UTF8, "application/json");

            // Act
            var response = await RequestHelper.PostAsyncWithHeaders(_client, "/Contact", stringContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(responseContent);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Please provide a valid phone number", errorResponse["PhoneNumber"]);
        }

        public void Dispose()
        {
            _factory.Dispose();
            _client.Dispose();
        }
    }
}
