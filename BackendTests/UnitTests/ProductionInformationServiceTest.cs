using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services;
using Xunit;

namespace BackendTests.UnitTests
{
    public class ProductionInformationServiceTest : UnitTestBase
    {
        /*[Fact]
        public async Task Create_production_information()
        {
            var options = CreateInMemoryDbOptions("Create_production_information");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                await service.CreateProductionInformationAsync(GenerateTestCustomer());
                context.SaveChanges();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            await using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(1, context.Customers.Count());
                Assert.Equal("TestCompany", context.Customers.Single().CompanyName);
            }
        }



        [Fact]
        public async Task Get_customer_by_name_and_get_customer_by_id()
        {
            var options = CreateInMemoryDbOptions("Get_customer_by_name_and_get_customer_by_id");

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                await service.CreateCustomerAsync(GenerateTestCustomer());
                context.SaveChanges();
            }


            await using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(1, context.Customers.Count());
                Assert.Equal("TestCompany", context.Customers.Single().CompanyName);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                var customer = await service.GetCustomerByNameAsync("TestCompany");
                var customer2 = await service.GetCustomerByIdAsync(customer.Id);
                Assert.Equal("TestTown", customer.CompanyTown);
                Assert.Equal("TestCompany", customer.CompanyName);
                Assert.Equal(customer.ToString(), customer2.ToString());
            }
        }

        [Fact]
        public async Task Getting_many_and_deleting_customers()
        {
            var options = CreateInMemoryDbOptions("Getting_many_and_deleting_customers");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                var customers = GenerateManyTestCustomers(5);
                customers.ForEach(async customer => await service.CreateCustomerAsync(customer));
                context.SaveChanges();
            }


            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                var customers = await service.GetCustomersAsync();
                Assert.Equal(5, customers.Count);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                var customer = await service.GetCustomerByNameAsync("TestCompany1");
                var success = await service.DeleteCustomerAsync(customer.Id);
                Assert.True(success);
                var customers = await service.GetCustomersAsync();
                Assert.Equal(4, customers.Count);
            }
        }

        [Fact]
        public async Task Updating_customer()
        {
            var options = CreateInMemoryDbOptions("Updating_customer");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                var customers = GenerateManyTestCustomers(5);
                customers.ForEach(async customer => await service.CreateCustomerAsync(customer));
                context.SaveChanges();
            }


            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                var customers = await service.GetCustomersAsync();
                Assert.Equal(5, customers.Count);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                var customer = await service.GetCustomerByNameAsync("TestCompany1");
                var updatedCustomer = new Customer
                {
                    Id = customer.Id,
                    CompanyName = "UpdatedCompany",
                    CompanyTown = "UpdatedTown",
                    CompanyPostalCode = "1234",
                    ContactPerson = customer.ContactPerson,
                    CompanyStreet = customer.CompanyStreet,
                    ContactNumber = customer.ContactNumber
                };
                var success = await service.UpdateCustomerAsync(updatedCustomer);
                Assert.True(success);
                updatedCustomer = await service.GetCustomerByNameAsync("UpdatedCompany");
                Assert.Equal(customer.Id, updatedCustomer.Id);
                Assert.Equal("UpdatedCompany", updatedCustomer.CompanyName);
                Assert.Equal("UpdatedTown", updatedCustomer.CompanyTown);
                Assert.Equal("1234", updatedCustomer.CompanyPostalCode);
            }
        }*/
    }
}
