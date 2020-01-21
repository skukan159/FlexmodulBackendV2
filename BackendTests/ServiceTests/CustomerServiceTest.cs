using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services;
using Xunit;

namespace BackendTests.ServiceTests
{
    public class CustomerServiceTest : ServiceTestBase
    {
        [Fact]
        public async Task Create_customer()
        {
            var options = CreateInMemoryDbOptions("Create_customer");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new CustomerService(context);
                var customer = GenerateTestCustomer();
                await service.CreateAsync(customer);
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
                var service = new CustomerService(context);
                var customer = GenerateTestCustomer();
                await service.CreateAsync(customer);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new CustomerService(context);
                var customer = await service.GetCustomerByNameAsync("TestCompany");
                var customer2 = await service.GetByIdAsync(customer.Id);
                Assert.Equal("TestTown", customer.CompanyTown);
                Assert.Equal("TestCompany", customer.CompanyName);
                Assert.Equal(customer.ToString(),customer2.ToString());
            }
        }

        [Fact]
        public async Task Getting_many_and_deleting_customers()
        {
            var options = CreateInMemoryDbOptions("Getting_many_and_deleting_customers");

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new CustomerService(context);
                var customers = GenerateManyTestCustomers(5);
                customers.ForEach(async customer => await service.CreateAsync(customer));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new CustomerService(context);
                var customers = await service.GetAsync();
                Assert.Equal(5, customers.Count);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new CustomerService(context);
                var customer = await service.GetCustomerByNameAsync("TestCompany1");
                var success = await service.DeleteAsync(customer);
                Assert.True(success);
                var customers = await service.GetAsync();
                Assert.Equal(4, customers.Count);
            }
        }

        [Fact]
        public async Task Updating_customer()
        {
            var options = CreateInMemoryDbOptions("Updating_customer");

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new CustomerService(context);
                var customers = GenerateManyTestCustomers(5);
                customers.ForEach(async customer => await service.CreateAsync(customer));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new CustomerService(context);
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
                var success = await service.UpdateAsync(updatedCustomer);
                Assert.True(success);
                updatedCustomer = await service.GetCustomerByNameAsync("UpdatedCompany");
                Assert.Equal(customer.Id, updatedCustomer.Id);
                Assert.Equal("UpdatedCompany", updatedCustomer.CompanyName);
                Assert.Equal("UpdatedTown", updatedCustomer.CompanyTown);
                Assert.Equal("1234", updatedCustomer.CompanyPostalCode);
            }
        }
    }
}
