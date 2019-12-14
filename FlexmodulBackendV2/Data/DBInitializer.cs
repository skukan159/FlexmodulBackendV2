using FlexmodulBackendV2.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexmodulBackendV2.Data
{
    public static class DbInitializer
    {
        public static void InitializeTestData(ApplicationDbContext context)
        {

            var customers = DataGenerator.GenerateCustomers();
            DbSeeder<Customer>.SeedDatabaseWithData(context, customers);

            var houseTypes = DataGenerator.GenerateFmHouseTypes();
            DbSeeder<FmHouseType>.SeedDatabaseWithData(context, houseTypes);

            var materials = DataGenerator.GenerateMaterials();
            DbSeeder<Material>.SeedDatabaseWithData(context, materials);

            var materialsOnHouseTypes = DataGenerator.GenerateMaterialOnHouseTypes(materials,houseTypes);
            DbSeeder<MaterialOnHouseType>.SeedDatabaseWithData(context, materialsOnHouseTypes);

            var fmHouses = DataGenerator.GenerateFmHouses(houseTypes);
            DbSeeder<FmHouse>.SeedDatabaseWithData(context, fmHouses);

            var newUser = new IdentityUser
            {
                Email = "test@test.com",
                UserName = "test@test.com"
            };
            context.Users.Add(newUser);
            context.SaveChanges();

            var productionInformations = DataGenerator.GenerateProductionInformation(fmHouses,houseTypes,customers,newUser);
            DbSeeder<ProductionInformation>.SeedDatabaseWithData(context, productionInformations);

            //Update houses with production information
            var houseType1 = fmHouses.Single(h => h.HouseType == houseTypes.Single(ht => ht.HouseType == 1));
            var houseType2 = fmHouses.Single(h => h.HouseType == houseTypes.Single(ht => ht.HouseType == 2));
            var houseType3 = fmHouses.Single(h => h.HouseType == houseTypes.Single(ht => ht.HouseType == 3));

            var rents = DataGenerator.GenerateRents(productionInformations,houseType1,houseType2,houseType3);
            DbSeeder<Rent>.SeedDatabaseWithData(context, rents);

            var rentalOverviews = DataGenerator.GenerateRentalOverviews(productionInformations,houseType1,houseType2,houseType3);
            DbSeeder<RentalOverview>.SeedDatabaseWithData(context, rentalOverviews);
        }

        public static async Task GenerateRoles(RoleManager<IdentityRole> roleManager)
        {
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

        // Possibly delete this later in the project
        public static async Task GenerateAdmin(UserManager<IdentityUser> userManager)
        {
            
            var newUser = new IdentityUser
            {
                Email = "admin@admin.com",
                UserName = "admin@admin.com"
            };
            const string password = "Admin123!";

            var existingUser = await userManager.FindByEmailAsync(newUser.Email);
            if (existingUser == null)
            {
                var createdUser = await userManager.CreateAsync(newUser, password);
                if (createdUser.Succeeded)
                    await userManager.AddToRoleAsync(newUser, "SuperAdmin");
            }
        }
    }
}
