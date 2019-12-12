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
            var options = CreateInMemoryDbOptions("Create_fmHouse2");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new FmHouseService(context);

                var house = GenerateFmHouse(GenerateFmHouseType());
                await service.CreateAsync(house);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new FmHouseService(context);
                
                Assert.Equal(1, context.FmHouses.Count());
                Assert.Equal(1, (await service.GetAsync()).Single().HouseType.HouseType);
            }
        }

        [Fact]
        public async Task Get_fmHouse()
        {
            var options = CreateInMemoryDbOptions("Get_fmHouse");

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new FmHouseService(context);

                var house = GenerateFmHouse(GenerateFmHouseType());
                await service.CreateAsync(house);
            }


            await using (var context = new ApplicationDbContext(options))
            {
                var service = new FmHouseService(context);
                var fmHouse = context.FmHouses.Single();
                var fmHouse2 = await service.GetByIdAsync(fmHouse.Id);
                Assert.Equal(1, fmHouse2.HouseType.HouseType);
                Assert.Equal(fmHouse2.ToString(), fmHouse.ToString());
            }
        }

        [Fact]
        public async Task Getting_many_and_deleting_fmHouses()
        {
            var options = CreateInMemoryDbOptions("Getting_many_and_deleting_fmHouses");

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new FmHouseService(context);

                var fmHouses = GenerateManyFmHouses(5);
                fmHouses.ForEach(async fmHouse => await service.CreateAsync(fmHouse));
            }


            await using (var context = new ApplicationDbContext(options))
            {
                var service = new FmHouseService(context);
                var fmHouses = await service.GetAsync();
                Assert.Equal(5, fmHouses.Count);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new FmHouseService(context);
                var fmHouse =  context.FmHouses.FirstOrDefault();
                var success = await service.DeleteAsync(fmHouse);
                Assert.True(success);
                var fmHouseTypes = await service.GetAsync();
                Assert.Equal(4, fmHouseTypes.Count);
            }
        }

        [Fact]
        public async Task Updating_fmHouse()
        {
            var options = CreateInMemoryDbOptions("Updating_fmHouse");

            await using (var context = new ApplicationDbContext(options))
            {
                var houseService = new FmHouseService(context);

                var fmHouses = GenerateManyFmHouses(5);
                fmHouses.ForEach(async fmHouse => await houseService.CreateAsync(fmHouse));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var houseService = new FmHouseService(context);
                var newHouseType = GenerateFmHouseType(8);

                var fmHouse = context.FmHouses.FirstOrDefault();
                fmHouse.HouseType = newHouseType;

                var success = await houseService.UpdateAsync(fmHouse);
                Assert.True(success);
                var updatedHouse = await houseService.GetByIdAsync(fmHouse.Id);
                Assert.Equal(fmHouse.Id, updatedHouse.Id);
                Assert.Equal(8, updatedHouse.HouseType.HouseType);
            }
        }
    }

}
