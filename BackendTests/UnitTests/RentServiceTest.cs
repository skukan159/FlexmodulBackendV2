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
    public class RentServiceTest : UnitTestBase
    {
       [Fact]
        public async Task Create_Rent()
        {
            var options = CreateInMemoryDbOptions("Create_Rent");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new RentService(context);
                var rent = GenerateTestRent();
                await service.CreateRentAsync(rent);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new RentService(context);
                Assert.Equal(1, context.Rents.Count());
                Assert.Equal(10000,
                    (await service.GetRentsAsync())
                    .Single().RentPrice);
            }
        }

        [Fact]
        public async Task GetRentById()
        {
            var options = CreateInMemoryDbOptions("GetRentById");

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new RentService(context);

                var rent = GenerateTestRent();
                await service.CreateRentAsync(rent);
            }


            await using (var context = new ApplicationDbContext(options))
            {
                var service = new RentService(context);
                var rent1 = context.Rents.Single();
                var rent2 = await service.GetRentByIdAsync(rent1.Id);
                Assert.Equal(rent2.ToString(), rent1.ToString());
            }
        }

        [Fact]
        public async Task DeletingRent()
        {
            var options = CreateInMemoryDbOptions("DeletingRent");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new RentService(context);

                var rents = GenerateManyTestRents(5);
                rents.ForEach(async r => await service.CreateRentAsync(r));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new RentService(context);
                var rents = await service.GetRentsAsync();
                Assert.Equal(5, rents.Count);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new RentService(context);
                var rent = context.Rents.FirstOrDefault();
                var success = await service.DeleteRentAsync(rent.Id);
                Assert.True(success);
                var rents = await service.GetRentsAsync();
                Assert.Equal(4, rents.Count);
            }
        }

        [Fact]
        public async Task Updating_Rent()
        {
            var options = CreateInMemoryDbOptions("Updating_Rent");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new RentService(context);

                var rents = GenerateManyTestRents(5);
                rents.ForEach(async r => await service.CreateRentAsync(r));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new RentService(context);
                var newRentPrice = 20000f;

                var rent = context.Rents.FirstOrDefault();
                rent.RentPrice = newRentPrice;

                var success = await service.UpdateRentAsync(rent);
                Assert.True(success);
                var updatedRent = await service.GetRentByIdAsync(rent.Id);
                Assert.Equal(rent.Id, updatedRent.Id);
                Assert.Equal(20000f, updatedRent.RentPrice);
            }
        }
    }
}
