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
    public class MaterialServiceTest : UnitTestBase
    {
        [Fact]
        public async Task Create_Material()
        {
            var options = CreateInMemoryDbOptions("Create_Material");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new MaterialService(context);
                var material = GenerateMaterial();
                await service.CreateMaterialAsync(material);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new MaterialService(context);
                Assert.Equal(1, context.Materials.Count());
                Assert.Equal(Material.HouseSections.Floor,
                    (await service.GetMaterialsAsync())
                    .Single().HouseSection);
            }
        }

        [Fact]
        public async Task GetMaterialById()
        {
            var options = CreateInMemoryDbOptions("GetMaterialById");

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new MaterialService(context);

                var material = GenerateMaterial();
                await service.CreateMaterialAsync(material);
            }


            await using (var context = new ApplicationDbContext(options))
            {
                var service = new MaterialService(context);
                var material1 = context.Materials.Single();
                var material2 = await service.GetMaterialByIdAsync(material1.Id);
                Assert.Equal(material2.ToString(), material1.ToString());
            }
        }

        [Fact]
        public async Task DeletingMaterial()
        {
            var options = CreateInMemoryDbOptions("DeletingMaterial");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new MaterialService(context);

                var materials = GenerateManyMaterials(5);
                materials.ForEach(async m => await service.CreateMaterialAsync(m));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new MaterialService(context);
                var materials = await service.GetMaterialsAsync();
                Assert.Equal(5, materials.Count);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new MaterialService(context);
                var material = context.Materials.FirstOrDefault();
                var success = await service.DeleteMaterialAsync(material.Id);
                Assert.True(success);
                var materials = await service.GetMaterialsAsync();
                Assert.Equal(4, materials.Count);
            }
        }

        [Fact]
        public async Task Updating_Material()
        {
            var options = CreateInMemoryDbOptions("Updating_Material");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new MaterialService(context);

                var materials = GenerateManyMaterials(5);
                materials.ForEach(async m => await service.CreateMaterialAsync(m));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new MaterialService(context);
                var newSupplier = "Kamstrup";

                var material = context.Materials.FirstOrDefault();
                material.Supplier = newSupplier;

                var success = await service.UpdateMaterialAsync(material);
                Assert.True(success);
                var updatedMaterial = await service.GetMaterialByIdAsync(material.Id);
                Assert.Equal(material.Id, updatedMaterial.Id);
                Assert.Equal("Kamstrup", updatedMaterial.Supplier);
            }
        }
    }
}