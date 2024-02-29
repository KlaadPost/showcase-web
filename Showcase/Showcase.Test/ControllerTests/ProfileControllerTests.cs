using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Showcase.Test.ControllerTests
{
    public class ProfileControllerTests : IDisposable
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public ProfileControllerTests()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true
            });
        }

        [Fact]
        public async Task ProfileActionReturnsView()
        {
            var response = await _client.GetAsync("/Profile");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public void Dispose()
        {
            _factory.Dispose();
            _client.Dispose();
        }
    }
}
