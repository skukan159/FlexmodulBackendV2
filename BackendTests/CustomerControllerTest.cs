using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Domain;
using FluentAssertions;
using Xunit;

namespace BackendTests
{
    public class CustomerControllerTest : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyCustomers_ReturnsEmptyResponse()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await testClient.GetAsync(ApiRoutes.Customers.GetAll);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Customer>>()).Should().BeEmpty();
        }
    }
}
