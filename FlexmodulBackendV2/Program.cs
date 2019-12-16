using System;
using System.Threading.Tasks;
using FlexmodulBackendV2.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FlexmodulBackendV2
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args)/*.ConfigureKestrel(o => { o.Limits.KeepAliveTimeout = TimeSpan.FromMilliseconds(1); })*/.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();

                var roleManager = serviceScope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider
                    .GetRequiredService<UserManager<IdentityUser>>();

                await dbContext.Database.MigrateAsync();

                try
                {
                    await DbInitializer.GenerateRoles(roleManager);
                    await DbInitializer.GenerateAdmin(userManager);
                    await DbInitializer.GenerateAdministrativeEmployee(userManager);
                    await DbInitializer.GenerateEmployee(userManager);
                    await DbInitializer.InitializeTestData(dbContext,userManager);
                }
                catch (Exception ex)
                {
                    var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }

            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
