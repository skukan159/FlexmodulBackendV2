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
    public class RentalOverviewServiceTest : UnitTestBase
    {
        [Fact]
        public async Task Create_RentalOverview()
        {
            var options = CreateInMemoryDbOptions("Create_RentalOverview");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new RentalOverviewService(context);
                var rentalOverview = GenerateRentalOverview();
                await service.CreateRentalOverviewAsync(rentalOverview);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new RentalOverviewService(context);
                Assert.Equal(1, context.RentalOverviews.Count());
                Assert.Equal(RentalOverview.PurchaseStatuses.Stock,
                    (await service.GetRentalOverviewsAsync())
                    .Single().PurchaseStatus);
            }
        }

        [Fact]
        public async Task GetRentalOverviewById()
        {
            var options = CreateInMemoryDbOptions("GetRentalOverviewById");

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new RentalOverviewService(context);

                var rentalOverview = GenerateRentalOverview();
                await service.CreateRentalOverviewAsync(rentalOverview);
            }


            await using (var context = new ApplicationDbContext(options))
            {
                var service = new RentalOverviewService(context);
                var rentalOverview1 = context.RentalOverviews.Single();
                var rentalOverview2 = await service.GetRentalOverviewByIdAsync(rentalOverview1.Id);
                Assert.Equal(rentalOverview2.ToString(), rentalOverview1.ToString());
            }
        }

        [Fact]
        public async Task DeletingRentalOverview()
        {
            var options = CreateInMemoryDbOptions("DeletingRentalOverview");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new RentalOverviewService(context);

                var rentalOverviews = GenerateManyRentalOverviews(5);
                rentalOverviews.ForEach(async ro => await service.CreateRentalOverviewAsync(ro));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new RentalOverviewService(context);
                var rentalOverviews = await service.GetRentalOverviewsAsync();
                Assert.Equal(5, rentalOverviews.Count);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new RentalOverviewService(context);
                var rentalOverview = context.RentalOverviews.FirstOrDefault();
                var success = await service.DeleteRentalOverviewAsync(rentalOverview.Id);
                Assert.True(success);
                var rentalOverviews = await service.GetRentalOverviewsAsync();
                Assert.Equal(4, rentalOverviews.Count);
            }
        }

        [Fact]
        public async Task Updating_RentalOverview()
        {
            var options = CreateInMemoryDbOptions("Updating_RentalOverview");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new RentalOverviewService(context);

                var rentalOverviews = GenerateManyRentalOverviews(5);
                rentalOverviews.ForEach(async m => await service.CreateRentalOverviewAsync(m));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new RentalOverviewService(context);
                var newPurchaseStatus = RentalOverview.PurchaseStatuses.ContractNotInitiated;

                var rentalOverview = context.RentalOverviews.FirstOrDefault();
                rentalOverview.PurchaseStatus = newPurchaseStatus;

                var success = await service.UpdateRentalOverviewAsync(rentalOverview);
                Assert.True(success);
                var updatedrentalOverview = await service.GetRentalOverviewByIdAsync(rentalOverview.Id);
                Assert.Equal(rentalOverview.Id, updatedrentalOverview.Id);
                Assert.Equal(RentalOverview.PurchaseStatuses.ContractNotInitiated, updatedrentalOverview.PurchaseStatus);
            }
        }
    }
}
