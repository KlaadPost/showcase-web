using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Showcase.Test.Helpers;
using Showcase.Web.Data;
using System.Net;
using Xunit;

namespace Showcase.Test.ControllerTests
{
    public class AdminControllerTests : IDisposable
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public AdminControllerTests()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true
            });
        }

        [Fact]
        public async Task IndexReturnsView()
        {
            var response = await _client.GetAsync("/Admin/Index");
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task EditReturnsView()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ShowcaseWebContext>();

                DbHelper.ReinitializeDbForTests(db);
            }

            var user = DbHelper.GetSeedingUser();

            // Acts
            var response = await _client.GetAsync("/Admin/Edit/" + user.Id);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        public void Dispose()
        {
            _factory.Dispose();
            _client.Dispose();
        }
    }
}
