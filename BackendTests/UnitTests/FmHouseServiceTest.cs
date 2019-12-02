using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services;
using Xunit;

namespace BackendTests.UnitTests
{
    public class FmHouseServiceTest : UnitTestBase
    {
        [Fact]
        public async Task Create_fmHouse()
        {
            var options = CreateInMemoryDbOptions("Create_fmHouse");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var houseService = new FmHouseService(context);
                var houseTypeService = new FmHouseTypeService(context);

                await houseTypeService.CreateFmHouseTypeAsync(GenerateFmHouseType(1));
                var houseType = await houseTypeService.GetFmHouseTypeByTypeAsync(1);
                await houseService.CreateFmHouseAsync(GenerateFmHouse(houseType,10));
                context.SaveChanges();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            await using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(1, context.FmHouses.Count());
                Assert.Equal(1, context.FmHouses.Single().HouseType.HouseType);
            }
        }



        [Fact]
        public async Task Get_fmHouse()
        {
            var options = CreateInMemoryDbOptions("Get_fmHouse");

            await using (var context = new ApplicationDbContext(options))
            {
                var houseService = new FmHouseService(context);
                var houseTypeService = new FmHouseTypeService(context);

                await houseTypeService.CreateFmHouseTypeAsync(GenerateFmHouseType(1));
                var houseType = await houseTypeService.GetFmHouseTypeByTypeAsync(1);
                await houseService.CreateFmHouseAsync(GenerateFmHouse(houseType, 10));
            }


            await using (var context = new ApplicationDbContext(options))
            {
                var service = new FmHouseService(context);
                var fmHouse = context.FmHouses.Single();
                var fmHouse2 = await service.GetFmHouseByIdAsync(fmHouse.Id);
                Assert.Equal(1, fmHouse2.HouseType.HouseType);
                Assert.Equal(fmHouse2.ToString(), fmHouse.ToString());
            }
        }

        [Fact]
        public async Task Getting_many_and_deleting_fmHouses()
        {
            var options = CreateInMemoryDbOptions("Getting_many_and_deleting_fmHouses");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var houseService = new FmHouseService(context);
                var houseTypeService = new FmHouseTypeService(context);

                var fmHouses = await GenerateManyFmHouses(5, context, houseTypeService);
                fmHouses.ForEach(async fmHouse => await houseService.CreateFmHouseAsync(fmHouse));
            }


            await using (var context = new ApplicationDbContext(options))
            {
                var service = new FmHouseService(context);
                var fmHouses = await service.GetFmHousesAsync();
                Assert.Equal(5, fmHouses.Count);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new FmHouseService(context);
                var fmHouse =  context.FmHouses.Single();
                var success = await service.DeleteFmHouseAsync(fmHouse.Id);
                Assert.True(success);
                var fmHouseTypes = await service.GetFmHousesAsync();
                Assert.Equal(4, fmHouseTypes.Count);
            }
        }

        [Fact]
        public async Task Updating_fmHouse()
        {
            var options = CreateInMemoryDbOptions("Updating_fmHouse");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var houseService = new FmHouseService(context);
                var houseTypeService = new FmHouseTypeService(context);

                var fmHouses = await GenerateManyFmHouses(5, context, houseTypeService);
                fmHouses.ForEach(async fmHouse=> await houseService.CreateFmHouseAsync(fmHouse));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var houseService = new FmHouseService(context);
                var houseTypeService = new FmHouseTypeService(context);
                var newHouseType = GenerateFmHouseType(8);
                await houseTypeService.CreateFmHouseTypeAsync(newHouseType);

                var fmHouse = context.FmHouses.Single();
                var updatedHouse = new FmHouse
                {
                    Id = fmHouse.Id,
                    HouseType = newHouseType
                };
                var success = await houseService.UpdateFmHouseAsync(updatedHouse);
                Assert.True(success);
                updatedHouse = await houseService.GetFmHouseByIdAsync(fmHouse.Id);
                Assert.Equal(fmHouse.Id, updatedHouse.Id);
                Assert.Equal(8, updatedHouse.HouseType.HouseType);
            }
        }
        public static async Task<List<FmHouse>> GenerateManyFmHouses(int count, ApplicationDbContext context, FmHouseTypeService service)
        {
            var fmHouses = new List<FmHouse>();

            for (var i = 1; i <= count; i++)
            {
                var houseType = GenerateFmHouseType(i);
                await service.CreateFmHouseTypeAsync(houseType);
                fmHouses.Add(GenerateFmHouse(houseType, i));
            }

            return fmHouses;
        }

    }

}
