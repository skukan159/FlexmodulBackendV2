using FlexmodulBackendV2.Domain;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace FlexmodulBackendV2.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeTestData(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {

            var customers = DataGenerator.GenerateCustomers();
            await DbSeeder<Customer>.SeedDatabaseWithData(context, customers);

            var houseTypes = DataGenerator.GenerateFmHouseTypes();
            await DbSeeder<FmHouseType>.SeedDatabaseWithData(context, houseTypes);

            var materials = DataGenerator.GenerateMaterials();
            await DbSeeder<Material>.SeedDatabaseWithData(context, materials);

            var materialsOnHouseTypes = DataGenerator.GenerateMaterialOnHouseTypes(materials,houseTypes);
            await DbSeeder<MaterialOnHouseType>.SeedDatabaseWithData(context, materialsOnHouseTypes);

            var fmHouses = DataGenerator.GenerateFmHouses(houseTypes);
            await DbSeeder<FmHouse>.SeedDatabaseWithData(context, fmHouses);


            var newUser = await userManager.FindByEmailAsync("test@test.com");
            if (newUser == null)
            {
                await GenerateTestUser(userManager);
                newUser = await userManager.FindByEmailAsync("test@test.com");
            }
            /*var newUser = new IdentityUser
            {
                Email = "test@test.com",
                UserName = "test@test.com"
            };
            context.Users.Add(newUser);
            await context.SaveChangesAsync();*/

            var productionInformations = DataGenerator.GenerateProductionInformation(fmHouses,houseTypes,customers,newUser);
            await DbSeeder<ProductionInformation>.SeedDatabaseWithData(context, productionInformations);

            //Update houses with production information
            var houseType1 = fmHouses.Single(h => h.HouseType == houseTypes.Single(ht => ht.HouseType == 1));
            var houseType2 = fmHouses.Single(h => h.HouseType == houseTypes.Single(ht => ht.HouseType == 2));
            var houseType3 = fmHouses.Single(h => h.HouseType == houseTypes.Single(ht => ht.HouseType == 3));

            var rents = DataGenerator.GenerateRents(productionInformations,houseType1,houseType2,houseType3);
            await DbSeeder<Rent>.SeedDatabaseWithData(context, rents);

            var rentalOverviews = DataGenerator.GenerateRentalOverviews(productionInformations,houseType1,houseType2,houseType3);
            await DbSeeder<RentalOverview>.SeedDatabaseWithData(context, rentalOverviews);
        }

        public static async Task GenerateRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Roles.SuperAdmin))
            {
                var superAdminRole = new IdentityRole(Roles.SuperAdmin);
                await roleManager.CreateAsync(superAdminRole);
            }
            if (!await roleManager.RoleExistsAsync(Roles.AdministrativeEmployee))
            {
                var administrativeEmployeeRole = new IdentityRole(Roles.AdministrativeEmployee);
                await roleManager.CreateAsync(administrativeEmployeeRole);
            }
            if (!await roleManager.RoleExistsAsync(Roles.Employee))
            {
                var employeeRole = new IdentityRole(Roles.Employee);
                await roleManager.CreateAsync(employeeRole);
            }
        }

        // Possibly delete this later in the project
        internal static async Task GenerateAdmin(UserManager<IdentityUser> userManager)
        {
            
            var newUser = new IdentityUser
            {
                Email = "admin@admin.com",
                UserName = "admin@admin.com"
            };
            const string password = "Admin123!";

            await GenerateUser(userManager, newUser, password, Roles.SuperAdmin);
        }

        internal static async Task GenerateAdministrativeEmployee(UserManager<IdentityUser> userManager)
        {

            var newUser = new IdentityUser
            {
                Email = "adminemployee@adminemployee.com",
                UserName = "adminemployee@adminemployee.com"
            };
            const string password = "AdminEmployee123!";

            await GenerateUser(userManager, newUser, password, Roles.AdministrativeEmployee);
        }
        internal static async Task GenerateEmployee(UserManager<IdentityUser> userManager)
        {

            var newUser = new IdentityUser
            {
                Email = "employee@employee.com",
                UserName = "employee@employee.com"
            };
            const string password = "Employee123!";

            await GenerateUser(userManager, newUser, password, Roles.Employee);
        }

        internal static async Task GenerateTestUser(UserManager<IdentityUser> userManager)
        {

            var newUser = new IdentityUser
            {
                Email = "test@test.com",
                UserName = "test@test.com"
            };
            const string password = "TestUser123!";

            await GenerateUser(userManager, newUser, password, Roles.AdministrativeEmployee);
        }

        internal static async Task GenerateUser(UserManager<IdentityUser> userManager, IdentityUser newUser,string password, string role)
        {


            var existingUser = await userManager.FindByEmailAsync(newUser.Email);
            if (existingUser == null)
            {
                var createdUser = await userManager.CreateAsync(newUser, password);
                if (createdUser.Succeeded)
                    await userManager.AddToRoleAsync(newUser, role);
            }
        }

    }
}
