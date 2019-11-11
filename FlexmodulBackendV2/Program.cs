using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulAPI.Data;
using FlexmodulBackendV2.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FlexmodulBackendV2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();

                await dbContext.Database.MigrateAsync();

                try
                {
                    DBInitializer.Initialize(dbContext);
                }
                catch (Exception ex)
                {
                    var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }

                //Todo: Uncomment this when the rest of the app works
                var roleManager = serviceScope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync("SuperAdmin"))
                {
                    var superAdminRole = new IdentityRole("SuperAdmin");
                    await roleManager.CreateAsync(superAdminRole);
                }
                if (!await roleManager.RoleExistsAsync("AdministrativeEmployee"))
                {
                    var administrativeEmployeeRole = new IdentityRole("AdministrativeEmployee");
                    await roleManager.CreateAsync(administrativeEmployeeRole);
                }
                if (!await roleManager.RoleExistsAsync("Employee"))
                {
                    var employeeRole = new IdentityRole("Employee");
                    await roleManager.CreateAsync(employeeRole);
                }
            }

            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
