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
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;

        public ProfileControllerTests()
        {
            _factory = new CustomWebApplicationFactory();
            _client =  _factory.CreateClient();
        }

        [Fact]
        public async Task IndexActionReturnsView()
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
