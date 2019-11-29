using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FlexmodulBackendV2;
using FlexmodulBackendV2.Contracts.V1;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace BackendTests.IntegrationTests
{
    public class BackEndConnectionTest
    {
        private readonly HttpClient _client;

        public BackEndConnectionTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
        }

        [Fact]
        public async Task CheckIfAbleToConnect()
        {
            var response = await _client.GetAsync(ApiRoutes.Values.GetAll);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
