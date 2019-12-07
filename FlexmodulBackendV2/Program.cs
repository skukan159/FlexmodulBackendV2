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
            var host = CreateWebHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();

                await dbContext.Database.MigrateAsync();

                try
                {
                    DbInitializer.Initialize(dbContext);
                }
                catch (Exception ex)
                {
                    var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }

                // Create User Roles
                var roleManager = serviceScope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

                var userManager = serviceScope.ServiceProvider
                    .GetRequiredService<UserManager<IdentityUser>>();

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

                //Delete this later in the project
                var newUser = new IdentityUser
                {
                    Email = "admin@admin.com",
                    UserName = "admin@admin.com"
                };
                var password = "Admin123!";

                var existingUser = await userManager.FindByEmailAsync(newUser.Email);
                if (existingUser == null)
                {
                    var createdUser = await userManager.CreateAsync(newUser, password);
                    if (createdUser.Succeeded)
                        await userManager.AddToRoleAsync(newUser, "SuperAdmin");
                }

            }

            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
