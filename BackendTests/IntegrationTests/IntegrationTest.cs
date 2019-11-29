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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BackendTests.IntegrationTests
{
    public class IntegrationTest : IDisposable
    {
        protected readonly HttpClient testClient;
        private readonly IServiceProvider _serviceProvider;
        private UserManager<IdentityUser> _userManager;

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
                            /*_userManager = new UserManager<IdentityUser>
                            {
                                Options = new IdentityOptions
                                {

                                }
                            };*/
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

        public virtual void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureDeleted();
        }


        /*private ApplicationDbContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);

            var testUser = new IdentityUser {Email = "test@test.com",};
            
            
            context.Users.Add();
            context.Categories.Add(wineCategory);

            context.Products.Add(new Product { Id = 1, Name = "La Trappe Isid'or", Category = beerCategory });
            context.Products.Add(new Product { Id = 2, Name = "St. Bernardus Abt 12", Category = beerCategory });
            context.Products.Add(new Product { Id = 3, Name = "Zundert", Category = beerCategory });
            context.Products.Add(new Product { Id = 4, Name = "La Trappe Blond", Category = beerCategory });
            context.Products.Add(new Product { Id = 5, Name = "La Trappe Bock", Category = beerCategory });
            context.Products.Add(new Product { Id = 6, Name = "St. Bernardus Tripel", Category = beerCategory });
            context.Products.Add(new Product { Id = 7, Name = "Grottenbier Bruin", Category = beerCategory });
            context.Products.Add(new Product { Id = 8, Name = "St. Bernardus Pater 6", Category = beerCategory });
            context.Products.Add(new Product { Id = 9, Name = "La Trappe Quadrupel", Category = beerCategory });
            context.Products.Add(new Product { Id = 10, Name = "Westvleteren 12", Category = beerCategory });
            context.Products.Add(new Product { Id = 11, Name = "Leffe Bruin", Category = beerCategory });
            context.Products.Add(new Product { Id = 12, Name = "Leffe Royale", Category = beerCategory });
            context.SaveChanges();

            return context;
        }*/

    }
}
