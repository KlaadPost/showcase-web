using Newtonsoft.Json;
using NuGet.Protocol;
using Showcase.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Showcase.Test.ControllerTests
{
    public class ContactControllerTests : IDisposable
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;

        public ContactControllerTests() 
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
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
            ContactModel contactModel = new ContactModel();
            contactModel.FirstName = "Jaap";
            contactModel.LastName = "Saus";
            contactModel.Email = "jaap@saus.nl";
            contactModel.PhoneNumber = "0600000002";

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(contactModel), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/Contact", stringContent);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ContactPostNoInputReturnsError()
        {
            ContactModel contactModel = new ContactModel();
            contactModel.FirstName = "";
            contactModel.LastName = "";
            contactModel.Email = "";
            contactModel.PhoneNumber = "";

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(contactModel), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/Contact", stringContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(responseContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Voornaam is verplicht", errorResponse["FirstName"]);
            Assert.Contains("Achternaam is verplicht", errorResponse["LastName"]);
            Assert.Contains("Email is verplicht", errorResponse["Email"]);
            Assert.Contains("Telefoonnummer is verplicht", errorResponse["PhoneNumber"]);
        }

        [Fact]
        public async Task ContactPostMaxLengthExceededReturnsError()
        {
            ContactModel contactModel = new ContactModel();
            contactModel.FirstName = new string('a', 256);
            contactModel.LastName = new string('b', 256);
            contactModel.Email = new string('c', 256) + "@example.com";
            contactModel.PhoneNumber = "0600000002";

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(contactModel), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/Contact", stringContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(responseContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Maximale lengte voornaam overschreden", errorResponse["FirstName"]);
            Assert.Contains("Maximale lengte achternaam overschreden", errorResponse["LastName"]);
            Assert.Contains("Maximale lengte email overschreden", errorResponse["Email"]);
        }

        [Fact]
        public async Task ContactPostInvalidEmailReturnsError()
        {
            ContactModel contactModel = new ContactModel();
            contactModel.FirstName = "Jaap";
            contactModel.LastName = "Saus";
            contactModel.Email = "invalid_email";
            contactModel.PhoneNumber = "0600000002";

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(contactModel), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/Contact", stringContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(responseContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Geef een geldig emailadres op", errorResponse["Email"]);
        }

        [Fact]
        public async Task ContactPostInvalidPhoneNumberReturnsError()
        {
            ContactModel contactModel = new ContactModel();
            contactModel.FirstName = "Jaap";
            contactModel.LastName = "Saus";
            contactModel.Email = "jaap@saus.nl";
            contactModel.PhoneNumber = "invalid_phone";

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(contactModel), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/Contact", stringContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(responseContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Geef een geldig telefoonnummer op", errorResponse["PhoneNumber"]);
        }

        public void Dispose()
        {
            _factory.Dispose();
            _client.Dispose();
        }
    }
}
