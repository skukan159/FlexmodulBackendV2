using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BackendTests.UnitTests
{
    public class UnitTestBase
    {

        public static DbContextOptions<ApplicationDbContext> CreateInMemoryDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging() //debugging purpose
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

        public static FmHouseType GenerateFmHouseType(int houseType = 1)
        {
            return new FmHouseType
            {
                HouseType = houseType,
            };
        }

        public static List<FmHouseType> GenerateManyFmHouseTypes(int count)
        {
            var fmHouseTypes = new List<FmHouseType>();

            for (var i = 1; i <= count; i++)
            {
                fmHouseTypes.Add(GenerateFmHouseType(i));
            }

            return fmHouseTypes;
        }

        public static FmHouse GenerateFmHouse(FmHouseType houseType, int squareMeters = 10)
        {
            return new FmHouse
            {
                HouseType = houseType,
                SquareMeters = squareMeters
            };
        }

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

        public static IdentityUser GenerateIdentityUser(string username = "testUser")
        {
            return new IdentityUser
            {
                Email = username
            };
        }

        public static ProductionInformation GenerateProductionInformation(int id = 0)
        {
            var customer = GenerateTestCustomer(index:id.ToString());
            var fmHouse = GenerateFmHouse(GenerateFmHouseType(houseType:id));
            var user = GenerateIdentityUser("testUser" + id);
            var date = DateTime.Now;

            return GenerateProductionInformation(customer, fmHouse, user, date);
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
    }
}
