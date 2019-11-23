using FlexmodulBackendV2;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FlexmodulBackendV2.Contracts.V1;
using FlexmodulBackendV2.Contracts.V1.Requests;
using FlexmodulBackendV2.Contracts.V1.Responses;
using FlexmodulBackendV2.Data;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BackendTests
{
    class IntegrationTest
    {
        protected readonly HttpClient testClient;

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
                Password = "SomePass123!"
            });

            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }

        protected async Task<PostResponse> CreatePostAsync(CreatePostRequest request)
        {
            var response = await testClient.PostAsJsonAsync(ApiRoutes.Posts.Create, request);
            return await response.Content.ReadAsAsync<PostResponse>();
        }
    }
}
