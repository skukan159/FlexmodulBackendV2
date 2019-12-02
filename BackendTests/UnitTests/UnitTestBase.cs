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

namespace BackendTests.UnitTests
{
    public class UnitTestBase
    {

        public static DbContextOptions<ApplicationDbContext> CreateInMemoryDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
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

        public static async Task<IdentityUser> RegisterUser(string username, string password, IdentityService service)
        {
            await service.RegisterAsync(username,password);
            return await service.GetUserByEmail(username);
        }

        /*public static ProductionInformation GenerateTestProductionInformation(
            Customer customer,
            List<AdditionalCost> additionalCosts,
            FmHouse house,
            )
            
        {
            return new ProductionInformation
            {
                Customer = customer,
                House = house,
                AdditionalCosts = additionalCost,

                IsActive = true,
                LastUpdatedDate = ,
                LastUpdatedBy = ,
                Ventilation = ,
                Note = ,
                ExteriorWalls = ,
                ProductionPrice = ,
                ProductionDate =
            };
        }*/
    }
}
