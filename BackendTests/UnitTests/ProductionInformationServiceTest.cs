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
       [Fact]
        public async Task Create_ProductionInformation()
        {
            var options = CreateInMemoryDbOptions("Create_ProductionInformation");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                var productionInformation = GenerateProductionInformation();
                await service.CreateProductionInformationAsync(productionInformation);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                Assert.Equal(1, context.ProductionInformations.Count());
                Assert.Equal("TestCompany0", 
                    (await service.GetProductionInformationsAsync())
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
                await service.CreateProductionInformationAsync(pi);
            }


            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                var pi1 = context.ProductionInformations.Single();
                var pi2 = await service.GetProductionInformationByIdAsync(pi1.Id);
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
                productionInformations.ForEach(async pi => await service.CreateProductionInformationAsync(pi));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                var productionInformations = await service.GetProductionInformationsAsync();
                Assert.Equal(5, productionInformations.Count);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                var productionInformation = context.ProductionInformations.FirstOrDefault();
                var success = await service.DeleteProductionInformationAsync(productionInformation.Id);
                Assert.True(success);
                var productionInformations = await service.GetProductionInformationsAsync();
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
                productionInformations.ForEach(async pi => await service.CreateProductionInformationAsync(pi));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new ProductionInformationService(context);
                var newCustomer = GenerateTestCustomer("8");

                var productionInformation = context.ProductionInformations.FirstOrDefault();
                productionInformation.Customer = newCustomer;

                var success = await service.UpdateProductionInformationAsync(productionInformation);
                Assert.True(success);
                var updatedProductionInformation = await service.GetProductionInformationByIdAsync(productionInformation.Id);
                Assert.Equal(productionInformation.Id, updatedProductionInformation.Id);
                Assert.Equal("TestCompany8", updatedProductionInformation.Customer.CompanyName);
            }
        }
    }
}
