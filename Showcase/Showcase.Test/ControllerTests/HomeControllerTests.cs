using System.Net;
using Xunit;

namespace Showcase.Test.ControllerTests
{
    public class HomeControllerTests : IDisposable
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;

        public HomeControllerTests()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetReturnsView()
        {
            var response = await _client.GetAsync("/Home/Index");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public void Dispose()
        {
            _factory.Dispose();
            _client.Dispose();
        }
    }
}
