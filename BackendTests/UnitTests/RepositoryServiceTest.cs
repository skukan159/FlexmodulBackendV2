using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services;
using FlexmodulBackendV2.Services.ServiceInterfaces;
using Xunit;

namespace BackendTests.UnitTests
{
    public class RepositoryServiceTest : UnitTestBase
    {
        [Fact]
        public async Task Create_Material()
        {
            var options = CreateInMemoryDbOptions("Create_Material");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<Material>(context);
                var material = GenerateMaterial();
                await service.CreateAsync(material);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<Material>(context);
                Assert.Equal(1, context.Materials.Count());
                Assert.Equal(Material.HouseSections.Floor,
                    (await service.GetAsync())
                    .Single().HouseSection);
            }
        }

        [Fact]
        public async Task GetMaterialById()
        {
            var options = CreateInMemoryDbOptions("GetMaterialById");

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<Material>(context);

                var material = GenerateMaterial();
                await service.CreateAsync(material);
            }


            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<Material>(context);
                var material1 = context.Materials.Single();
                var material2 = await service.GetByIdAsync(material1.Id);
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
                var service = new Repository<Material>(context);

                var materials = GenerateManyMaterials(5);
                materials.ForEach(async m => await service.CreateAsync(m));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<Material>(context);
                var materials = await service.GetAsync();
                Assert.Equal(5, materials.Count);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<Material>(context);
                var material = context.Materials.FirstOrDefault();
                var success = await service.DeleteAsync(material);
                Assert.True(success);
                var materials = await service.GetAsync();
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
                var service = new Repository<Material>(context);

                var materials = GenerateManyMaterials(5);
                materials.ForEach(async m => await service.CreateAsync(m));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<Material>(context);
                var newSupplier = "Kamstrup";

                var material = context.Materials.FirstOrDefault();
                material.Supplier = newSupplier;

                var success = await service.UpdateAsync(material);
                Assert.True(success);
                var updatedMaterial = await service.GetByIdAsync(material.Id);
                Assert.Equal(material.Id, updatedMaterial.Id);
                Assert.Equal("Kamstrup", updatedMaterial.Supplier);
            }
        }


        //Materials on house types

        [Fact]
        public async Task Create_MaterialOnHouseType()
        {
            var options = CreateInMemoryDbOptions("Create_MaterialOnHouseType");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<MaterialOnHouseType>(context);
                var materialOnHouseType = GenerateMaterialOnHouseType();
                await service.CreateAsync(materialOnHouseType);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<MaterialOnHouseType>(context);
                Assert.Equal(1, context.MaterialOnHouseTypes.Count());
                Assert.Equal(1, (await service.GetAsync())
                    .Single().MaterialAmount);
            }
        }

        [Fact]
        public async Task Get_MaterialOnHouseTypeById()
        {
            var options = CreateInMemoryDbOptions("Get_MaterialOnHouseTypeById");

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<MaterialOnHouseType>(context);

                var moht = GenerateMaterialOnHouseType();
                await service.CreateAsync(moht);
            }


            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<MaterialOnHouseType>(context);
                var moht1 = context.MaterialOnHouseTypes.Single();
                var moht2 = await service.GetByIdAsync(moht1.Id);
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
                var service = new Repository<MaterialOnHouseType>(context);

                var materiaOnHouseTypes = GenerateManyMaterialOnHouseTypes(5);
                materiaOnHouseTypes.ForEach(async moht => await service.CreateAsync(moht));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<MaterialOnHouseType>(context);
                var materialOnHouseTypes = await service.GetAsync();
                Assert.Equal(5, materialOnHouseTypes.Count);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<MaterialOnHouseType>(context);
                var moht = context.MaterialOnHouseTypes.FirstOrDefault();
                var success = await service.DeleteAsync(moht);
                Assert.True(success);
                var materialsOnHouseTypes = await service.GetAsync();
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
                var service = new Repository<MaterialOnHouseType>(context);

                var materialsOnHouseType = GenerateManyMaterialOnHouseTypes(5);
                materialsOnHouseType.ForEach(async moht => await service.CreateAsync(moht));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<MaterialOnHouseType>(context);
                var newAmount = 9;

                var moht = context.MaterialOnHouseTypes.FirstOrDefault();
                moht.MaterialAmount = newAmount;

                var success = await service.UpdateAsync(moht);
                Assert.True(success);
                var updatedMoht = await service.GetByIdAsync(moht.Id);
                Assert.Equal(moht.Id, updatedMoht.Id);
                Assert.Equal(9, updatedMoht.MaterialAmount);
            }
        }

        //Rents

        [Fact]
        public async Task Create_Rent()
        {
            var options = CreateInMemoryDbOptions("Create_Rent");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<Rent>(context);
                var rent = GenerateTestRent();
                await service.CreateAsync(rent);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<Rent>(context);
                Assert.Equal(1, context.Rents.Count());
                Assert.Equal(10000,
                    (await service.GetAsync())
                    .Single().RentPrice);
            }
        }

        [Fact]
        public async Task GetRentById()
        {
            var options = CreateInMemoryDbOptions("GetRentById");

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<Rent>(context);

                var rent = GenerateTestRent();
                await service.CreateAsync(rent);
            }


            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<Rent>(context);
                var rent1 = context.Rents.Single();
                var rent2 = await service.GetByIdAsync(rent1.Id);
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
                var service = new Repository<Rent>(context);

                var rents = GenerateManyTestRents(5);
                rents.ForEach(async r => await service.CreateAsync(r));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<Rent>(context);
                var rents = await service.GetAsync();
                Assert.Equal(5, rents.Count);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<Rent>(context);
                var rent = context.Rents.FirstOrDefault();
                var success = await service.DeleteAsync(rent);
                Assert.True(success);
                var rents = await service.GetAsync();
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
                var service = new Repository<Rent>(context);

                var rents = GenerateManyTestRents(5);
                rents.ForEach(async r => await service.CreateAsync(r));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<Rent>(context);
                var newRentPrice = 20000f;

                var rent = context.Rents.FirstOrDefault();
                rent.RentPrice = newRentPrice;

                var success = await service.UpdateAsync(rent);
                Assert.True(success);
                var updatedRent = await service.GetByIdAsync(rent.Id);
                Assert.Equal(rent.Id, updatedRent.Id);
                Assert.Equal(20000f, updatedRent.RentPrice);
            }
        }



        //Rental Overviews
        [Fact]
        public async Task Create_RentalOverview()
        {
            var options = CreateInMemoryDbOptions("Create_RentalOverview");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<RentalOverview>(context);
                var rentalOverview = GenerateRentalOverview();
                await service.CreateAsync(rentalOverview);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<RentalOverview>(context);
                Assert.Equal(1, context.RentalOverviews.Count());
                Assert.Equal(RentalOverview.PurchaseStatuses.Stock,
                    (await service.GetAsync())
                    .Single().PurchaseStatus);
            }
        }

        [Fact]
        public async Task GetRentalOverviewById()
        {
            var options = CreateInMemoryDbOptions("GetRentalOverviewById");

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<RentalOverview>(context);

                var rentalOverview = GenerateRentalOverview();
                await service.CreateAsync(rentalOverview);
            }


            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<RentalOverview>(context);
                var rentalOverview1 = context.RentalOverviews.Single();
                var rentalOverview2 = await service.GetByIdAsync(rentalOverview1.Id);
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
                var service = new Repository<RentalOverview>(context);

                var rentalOverviews = GenerateManyRentalOverviews(5);
                rentalOverviews.ForEach(async ro => await service.CreateAsync(ro));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<RentalOverview>(context);
                var rentalOverviews = await service.GetAsync();
                Assert.Equal(5, rentalOverviews.Count);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<RentalOverview>(context);
                var rentalOverview = context.RentalOverviews.FirstOrDefault();
                var success = await service.DeleteAsync(rentalOverview);
                Assert.True(success);
                var rentalOverviews = await service.GetAsync();
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
                var service = new Repository<RentalOverview>(context);

                var rentalOverviews = GenerateManyRentalOverviews(5);
                rentalOverviews.ForEach(async m => await service.CreateAsync(m));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new Repository<RentalOverview>(context);
                var newPurchaseStatus = RentalOverview.PurchaseStatuses.ContractNotInitiated;

                var rentalOverview = context.RentalOverviews.FirstOrDefault();
                rentalOverview.PurchaseStatus = newPurchaseStatus;

                var success = await service.UpdateAsync(rentalOverview);
                Assert.True(success);
                var updatedrentalOverview = await service.GetByIdAsync(rentalOverview.Id);
                Assert.Equal(rentalOverview.Id, updatedrentalOverview.Id);
                Assert.Equal(RentalOverview.PurchaseStatuses.ContractNotInitiated, updatedrentalOverview.PurchaseStatus);
            }
        }
    }
}
