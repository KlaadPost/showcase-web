using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Showcase.Test.Helpers;
using Showcase.Test.Utilities;
using Showcase.Web.Data;
using Showcase.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Showcase.Test.ControllerTests
{
    public class ChatControllerTests : IDisposable
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public ChatControllerTests()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true
            }); 
        }

        [Fact]
        public async Task ChatActionReturnsView()
        {
            // Arrange
            string uri = "/Chat/Index";

            // Act
            var response = await _client.GetAsync(uri);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetsMessagesFromDb()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ShowcaseWebContext>();

                DbHelper.ReinitializeDbForTests(db);
            }

            // Act
            var response = await _client.GetAsync("/Chat/Messages");
            var content = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<List<ChatMessage>>(content);
            var expectedMessages = DbHelper.GetSeedingMessages();

            // Assert
            Assert.NotNull(content);
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expectedMessages.Count, responseObject.Count);

            for (int i = 0; i < expectedMessages.Count; i++)
            {
                Assert.Equal(expectedMessages[i].Message, responseObject[i].Message);
                Assert.Equal(expectedMessages[i].SenderId, responseObject[i].SenderId);
                Assert.Equal(expectedMessages[i].SenderName, responseObject[i].SenderName);
            }
        }


        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
