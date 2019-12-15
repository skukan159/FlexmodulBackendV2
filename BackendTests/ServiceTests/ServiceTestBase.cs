using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BackendTests.UnitTests
{
    public class ServiceTestBase
    {
        public static DbContextOptions<ApplicationDbContext> CreateInMemoryDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging()
                .Options;
        }

        public static Customer GenerateTestCustomer(string index = "",
            string companyName = "TestCompany",
            string postalCode = "99999",
            string street = "TestStreet",
            string town = "TestTown",
            string contactNumber = "123456789",
            string contactPerson = "TestPerson")
        {
            return new Customer
            {
                CompanyName = companyName + index,
                CompanyPostalCode = postalCode + index,
                CompanyStreet = street + index,
                CompanyTown = town + index,
                ContactNumber = contactNumber + index,
                ContactPerson = contactPerson + index
            };
        }

        public static List<Customer> GenerateManyTestCustomers(int count)
        {
            var customers = new List<Customer>();

            for (var i = 1; i <= count; i++)
            {
                customers.Add(GenerateTestCustomer(i.ToString()));
            }

            return customers;
        }

        public static FmHouseType GenerateFmHouseType(int houseType = 1) => 
            new FmHouseType { HouseType = houseType};
        

        public static List<FmHouseType> GenerateManyFmHouseTypes(int count)
        {
            var fmHouseTypes = new List<FmHouseType>();

            for (var i = 1; i <= count; i++)
            {
                fmHouseTypes.Add(GenerateFmHouseType(i));
            }

            return fmHouseTypes;
        }

        public static FmHouse GenerateFmHouse(FmHouseType houseType, int squareMeters = 10) => 
            new FmHouse { HouseType = houseType, SquareMeters = squareMeters};

        public static List<FmHouse> GenerateManyFmHouses(int count)
        {
            var fmHouses = new List<FmHouse>();

            for (var i = 1; i <= count; i++)
            {

                fmHouses.Add(GenerateFmHouse(GenerateFmHouseType(i), i * 10));
            }

            return fmHouses;
        }

        public static async Task<IdentityUser> RegisterUser
            (IdentityService service, string username = "testUser", string password = "testPassword")
        {
            await service.RegisterAsync(username,password);
            return await service.GetUserByEmail(username);
        }

        public static IdentityUser GenerateIdentityUser(string username = "testUser") => 
            new IdentityUser { Email = username };
        
        public static ProductionInformation GenerateProductionInformation(int id = 0)
        {
            var customer = GenerateTestCustomer(index:id.ToString());
            var fmHouse = GenerateFmHouse(GenerateFmHouseType(houseType:id));
            var user = GenerateIdentityUser("testUser" + id);
            var date = DateTime.Now;

            return GenerateProductionInformation(customer, fmHouse, user, date);
        }

        public static ProductionInformation GenerateProductionInformation(
            Customer customer, FmHouse house, IdentityUser updatedBy,
            DateTime productionDate, float productionPrice = 100000f, bool isActive = true)
        {
            return new ProductionInformation
            {
                Customer = customer,
                House = house,
                LastUpdatedDate = DateTime.Now,
                LastUpdatedBy = updatedBy,
                ProductionPrice = productionPrice,
                ProductionDate = productionDate,
                IsActive = isActive
            };
        }

        public static List<ProductionInformation> GenerateManyProductionInformations(int count)
        {
            var productionInformations = new List<ProductionInformation>();
            for (var i = 0; i < count; i++)
            {
                productionInformations.Add(GenerateProductionInformation(i));
            }

            return productionInformations;
        }

        public static Rent GenerateTestRent(int index = 0)
        {
            var productionInfo = GenerateProductionInformation(index);
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;
            var rentPrice = 10000 + 10 * index;
            var insurancePrice = 5000 + 10 * index;

            return GenerateTestRent(productionInfo, startDate, endDate, rentPrice, insurancePrice);
        }
        public static Rent GenerateTestRent
            (ProductionInformation productionInformation, DateTime startDate, DateTime endDate, float rentPrice = 10000, float insurancePrice = 5000)
        {
            return new Rent
            {
                ProductionInformation = productionInformation,
                RentPrice = rentPrice,
                InsurancePrice = insurancePrice,
                StartDate = startDate,
                EndDate = endDate,
            };
        }

        public static List<Rent> GenerateManyTestRents(int count)
        {
            var rents = new List<Rent>();
            for (var i = 0; i < count; i++)
            {
                rents.Add(GenerateTestRent(i));
            }

            return rents;
        }

        public static Material GenerateMaterial(int index)
        {

            var category = "testCategory" + index;
            var houseSection = Material.HouseSections.Floor;
            var name = "testName" + index;
            var pricePerUnit = 100 + index;
            var supplier = "testSupplier" + index;
            var units = "testUnit" + index;

            return GenerateMaterial(category, houseSection, name, pricePerUnit, supplier, units);
        }

        public static Material GenerateMaterial
            (string category = "testCategory", Material.HouseSections houseSection = Material.HouseSections.Floor,
            string name = "testName", float pricePerUnit = 100, string supplier = "testSupplier", string units = "testUnit")
        {
            return new Material
            {
                Category = category,
                HouseSection = houseSection,
                Name = name,
                PricePerUnit = pricePerUnit,
                Supplier = supplier,
                Units = units
            };
        }

        public static List<Material> GenerateManyMaterials(int count)
        {
            var materials = new List<Material>();
            for (var i = 0; i < count; i++)
            {
                materials.Add(GenerateMaterial(i));
            }

            return materials;
        }

        public static MaterialOnHouseType GenerateMaterialOnHouseType(int index = 0)
        {
            var houseType = GenerateFmHouseType(index);
            var material = GenerateMaterial(index);
            var amount = index + 1;

            return GenerateMaterialOnHouseType(houseType, material, amount);

        }

        public static MaterialOnHouseType GenerateMaterialOnHouseType(FmHouseType houseType, Material material, int amount = 10) =>
            new MaterialOnHouseType { FmHouseType = houseType, Material = material, MaterialAmount = amount};
        

        public static List<MaterialOnHouseType> GenerateManyMaterialOnHouseTypes(int count)
        {
            var materialsOnHouseTypes = new List<MaterialOnHouseType>();
            for (var i = 0; i < count; i++)
            {
                materialsOnHouseTypes.Add(GenerateMaterialOnHouseType(i));
            }

            return materialsOnHouseTypes;
        }

        public static RentalOverview GenerateRentalOverview(int index = 0)
        {
            var productionInformations = GenerateManyProductionInformations(index+1);
            var estimatedPrice = 40000f + 10000.5f * index;
            var purchaseStatus = RentalOverview.PurchaseStatuses.Stock;
            int setupAddressPostalCode = 1000 + index;
            string setupAddressStreet = "TestStreet" + index;
            string setupAddressTown = "TestTown" + index;


            return GenerateRentalOverview
                (productionInformations,estimatedPrice,purchaseStatus,setupAddressPostalCode,setupAddressStreet,setupAddressTown);

        }

        public static RentalOverview GenerateRentalOverview
            (ICollection<ProductionInformation> productionInformations, float estimatedPrice, 
            RentalOverview.PurchaseStatuses purchaseStatus,
            int setupAddressPostalCode, string setupAddressStreet, string setupAddressTown)
        {
            return new RentalOverview
            {
                ProductionInformations = productionInformations,
                EstimatedPrice = estimatedPrice,
                PurchaseStatus = purchaseStatus,
                SetupAddressPostalCode = setupAddressPostalCode,
                SetupAddressStreet = setupAddressStreet,
                SetupAddressTown = setupAddressTown
            };
        }

        public static List<RentalOverview> GenerateManyRentalOverviews(int count)
        {
            var rentalOverviews = new List<RentalOverview>();
            for (var i = 0; i < count; i++)
            {
                rentalOverviews.Add(GenerateRentalOverview(i));
            }

            return rentalOverviews;
        }

    }
}
