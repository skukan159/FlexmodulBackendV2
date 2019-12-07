using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Services;
using Xunit;

namespace BackendTests.UnitTests
{
    public class MaterialsOnHouseTypesServiceTest : UnitTestBase
    {
        [Fact]
        public async Task Create_MaterialOnHouseType()
        {
            var options = CreateInMemoryDbOptions("Create_MaterialOnHouseType");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new MaterialOnHouseTypesService(context);
                var materialOnHouseType = GenerateMaterialOnHouseType();
                await service.CreateMaterialOnHouseTypeAsync(materialOnHouseType);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new MaterialOnHouseTypesService(context);
                Assert.Equal(1, context.MaterialOnHouseTypes.Count());
                Assert.Equal(1, (await service.GetMaterialOnHouseTypesAsync())
                    .Single().MaterialAmount);
            }
        }

        [Fact]
        public async Task Get_MaterialOnHouseTypeById()
        {
            var options = CreateInMemoryDbOptions("Get_MaterialOnHouseTypeById");

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new MaterialOnHouseTypesService(context);

                var moht = GenerateMaterialOnHouseType();
                await service.CreateMaterialOnHouseTypeAsync(moht);
            }


            await using (var context = new ApplicationDbContext(options))
            {
                var service = new MaterialOnHouseTypesService(context);
                var moht1 = context.MaterialOnHouseTypes.Single();
                var moht2 = await service.GetMaterialOnHouseTypeByIdAsync(moht1.Id);
                Assert.Equal(moht2.ToString(), moht1.ToString());
            }
        }

        [Fact]
        public async Task DeletingMaterialOnHouseType()
        {
            var options = CreateInMemoryDbOptions("DeletingMaterialOnHouseType");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new MaterialOnHouseTypesService(context);

                var materiaOnHouseTypes = GenerateManyMaterialOnHouseTypes(5);
                materiaOnHouseTypes.ForEach(async moht => await service.CreateMaterialOnHouseTypeAsync(moht));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new MaterialOnHouseTypesService(context);
                var materialOnHouseTypes = await service.GetMaterialOnHouseTypesAsync();
                Assert.Equal(5, materialOnHouseTypes.Count);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new MaterialOnHouseTypesService(context);
                var moht = context.MaterialOnHouseTypes.FirstOrDefault();
                var success = await service.DeleteMaterialOnHouseTypeAsync(moht.Id);
                Assert.True(success);
                var materialsOnHouseTypes = await service.GetMaterialOnHouseTypesAsync();
                Assert.Equal(4, materialsOnHouseTypes.Count);
            }
        }

        [Fact]
        public async Task Updating_MaterialOnHouseType()
        {
            var options = CreateInMemoryDbOptions("Updating_MaterialOnHouseType");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new MaterialOnHouseTypesService(context);

                var materialsOnHouseType = GenerateManyMaterialOnHouseTypes(5);
                materialsOnHouseType.ForEach(async moht => await service.CreateMaterialOnHouseTypeAsync(moht));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new MaterialOnHouseTypesService(context);
                var newAmount = 9;

                var moht = context.MaterialOnHouseTypes.FirstOrDefault();
                moht.MaterialAmount = newAmount;

                var success = await service.UpdateMaterialOnHouseTypeAsync(moht);
                Assert.True(success);
                var updatedMoht = await service.GetMaterialOnHouseTypeByIdAsync(moht.Id);
                Assert.Equal(moht.Id, updatedMoht.Id);
                Assert.Equal(9, updatedMoht.MaterialAmount);
            }
        }
    }
}
