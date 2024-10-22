﻿using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Services;
using Xunit;

namespace BackendTests.ServiceTests
{
    public class ProductionInformationServiceTest : ServiceTestBase
    {
       [Fact]
        public async Task Create_ProductionInformation()
        {
            var options = CreateInMemoryDbOptions("Create_ProductionInformation");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                var productionInformation = GenerateProductionInformation();
                await service.CreateAsync(productionInformation);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                Assert.Equal(1, context.ProductionInformations.Count());
                Assert.Equal("TestCompany0", 
                    (await service.GetAsync())
                    .Single().Customer.CompanyName);
            }
        }

        [Fact]
        public async Task Get_productionInformationById()
        {
            var options = CreateInMemoryDbOptions("Get_productionInformationById");

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);

                var pi = GenerateProductionInformation();
                await service.CreateAsync(pi);
            }


            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                var pi1 = context.ProductionInformations.Single();
                var pi2 = await service.GetByIdAsync(pi1.Id);
                Assert.Equal(pi2.ToString(), pi1.ToString());
            }
        }

        [Fact]
        public async Task DeletingProductionInformation()
        {
            var options = CreateInMemoryDbOptions("DeletingProductionInformation");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);

                var productionInformations = GenerateManyProductionInformations(5);
                productionInformations.ForEach(async pi => await service.CreateAsync(pi));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                var productionInformations = await service.GetAsync();
                Assert.Equal(5, productionInformations.Count);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                var productionInformation = context.ProductionInformations.FirstOrDefault();
                var success = await service.DeleteAsync(productionInformation);
                Assert.True(success);
                var productionInformations = await service.GetAsync();
                Assert.Equal(4, productionInformations.Count);
            }
        }

        [Fact]
        public async Task Updating_ProductionInformation()
        {
            var options = CreateInMemoryDbOptions("Updating_ProductionInformation");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);

                var productionInformations = GenerateManyProductionInformations(5);
                productionInformations.ForEach(async pi => await service.CreateAsync(pi));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                var newCustomer = GenerateTestCustomer("8");

                var productionInformation = context.ProductionInformations.FirstOrDefault();
                productionInformation.Customer = newCustomer;

                var success = await service.UpdateAsync(productionInformation);
                Assert.True(success);
                var updatedProductionInformation = await service.GetByIdAsync(productionInformation.Id);
                Assert.Equal(productionInformation.Id, updatedProductionInformation.Id);
                Assert.Equal("TestCompany8", updatedProductionInformation.Customer.CompanyName);
            }
        }
    }
}
