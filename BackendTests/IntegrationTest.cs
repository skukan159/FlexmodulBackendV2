using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FlexmodulBackendV2;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.Requests;
using FlexmodulBackendV2.Contracts.V1.Requests.Customer;
using FlexmodulBackendV2.Contracts.V1.Responses;
using FlexmodulBackendV2.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BackendTests
{
    public class IntegrationTest : IDisposable
    {
        protected readonly HttpClient testClient;
        private readonly IServiceProvider _serviceProvider;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(ApplicationDbContext));
                        services.AddDbContext<ApplicationDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("testDb");
                        });

                    });
                });
            _serviceProvider = appFactory.Services;
            testClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await testClient.PostAsJsonAsync(ApiRoutes.Identity.Register, new UserRegistrationRequest
            {
                Email = "test@intergration.com",
                Password = "Test123!",
                SecretPassword = "SuperSecretPassword"
            });

            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }

        protected async Task<CustomerResponse> CreateCustomerAsync(CreateCustomerRequest request)
        {
            var response = await testClient.PostAsJsonAsync(ApiRoutes.Customers.Create, request);
            return await response.Content.ReadAsAsync<CustomerResponse>();
        }

        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureDeleted();
        }
    }
}
