using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Controllers.V1;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace BackendTests.IntegrationTests
{
    public class IdentityControllerTest : IntegrationTest
    {

        /*[Fact]
        public async Task RegisterNewUser_ReturnsHttpStatusOK_WhenValidModelPosted()
        {
            //Arrange
            var mockStore = Mock.Of<IUserStore<IdentityUser>>();
            var mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore, null, null, null, null, null, null, null, null);

            mockUserManager
                .Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var sut = new IdentityController(new IdentityService(userManager: mockUserManager.Object,));
            var input = new NewUserInputBuilder().Build();

            //Act
            var actual = await sut.RegisterNewUser(input);

            //Assert
            actual
                .Should().NotBeNull()
                .And.Match<HttpResponseMessage>(_ => _.IsSuccessStatusCode == true);
        }*/

        /*[Fact]
        public void RegisterNewUser_ReturnsHttpStatusOK_WhenValidModelPosted()
        {
            var mockStore = new Mock<IUserStore<IdentityUser>>();
            var mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);

            IdentityUser testUser = new IdentityUser { UserName = "user@test.com" };

            mockStore.Setup(x => x.CreateAsync(testUser, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            mockStore.Setup(x => x.FindByNameAsync(testUser.UserName, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(testUser));


            mockUserManager.Setup(x => x.CreateAsync(testUser).Result).Returns(new IdentityResult());

            IdentityService = new IdentityService(mockUserManager.Object);
            IdentityController ic = new IdentityController();
            var input = new NewUserInputBuilder().Build();
            ic.RegisterNewUser(input);

        }*/

/*
        [Fact]
        public async Task GetAll_WithoutAnyCustomers_ReturnsEmptyResponse()
        {
            // Arrange
            //await AuthenticateAsync();

            // Act
            var request = "api/v1/identity/getUserById/7e48cf1a-bda7-4b22-b686-01f94225b717";
            //var response = await testClient.GetAsync(ApiRoutes.Identity.GetByUsername + "/admin@admin.com");
            var response = await testClient.GetAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Customer>>()).Should().BeEmpty();
        }*/
    }
}
