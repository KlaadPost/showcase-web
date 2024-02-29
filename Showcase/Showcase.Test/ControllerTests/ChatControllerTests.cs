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

        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
