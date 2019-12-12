using System.Linq;
using System.Threading.Tasks;
using FlexmodulBackendV2.Data;
using FlexmodulBackendV2.Domain;
using FlexmodulBackendV2.Services;
using Xunit;

namespace BackendTests.UnitTests
{
    public class FmHouseTypeServiceTest : UnitTestBase
    {
        [Fact]
        public async Task Create_fmHouseType()
        {
            var options = CreateInMemoryDbOptions("Create_fmHouseType");

            // Run the test against one instance of the context
            await using (var context = new ApplicationDbContext(options))
            {
                var service = new FmHouseTypeService(context);
                await service.CreateAsync(GenerateFmHouseType());
            }

            // Use a separate instance of the context to verify correct data was saved to database
            await using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(1, context.FmHouseTypes.Count());
                Assert.Equal(1, context.FmHouseTypes.Single().HouseType);
            }
        }

        [Fact]
        public async Task Get_fmHouseType()
        {
            var options = CreateInMemoryDbOptions("Get_fmHouseType");

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new FmHouseTypeService(context);
                await service.CreateAsync(GenerateFmHouseType());
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new FmHouseTypeService(context);
                var fmHouseType = await service.GetFmHouseTypeByTypeAsync(1);
                var fmHouseType2 = await service.GetByIdAsync(fmHouseType.Id);
                Assert.Equal(1, fmHouseType.HouseType);
                Assert.Equal(1, fmHouseType2.HouseType);
                Assert.Equal(fmHouseType.ToString(), fmHouseType2.ToString());
            }
        }

        [Fact]
        public async Task Getting_many_and_deleting_fmHouseTypes()
        {
            var options = CreateInMemoryDbOptions("Getting_many_and_deleting_fmHouseTypes");

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new FmHouseTypeService(context);
                var fmHouseTypes = GenerateManyFmHouseTypes(5);
                fmHouseTypes.ForEach(async fmHouseType => await service.CreateAsync(fmHouseType));
            }


            await using (var context = new ApplicationDbContext(options))
            {
                var service = new FmHouseTypeService(context);
                var fmHouseTypes = await service.GetAsync();
                Assert.Equal(5, fmHouseTypes.Count);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new FmHouseTypeService(context);
                var fmHouseType = await service.GetFmHouseTypeByTypeAsync(1);
                var success = await service.DeleteAsync(fmHouseType);
                Assert.True(success);
                var fmHouseTypes = await service.GetAsync();
                Assert.Equal(4, fmHouseTypes.Count);
            }
        }

        [Fact]
        public async Task Updating_fmHouseType()
        {
            var options = CreateInMemoryDbOptions("Updating_fmHouseType");

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new FmHouseTypeService(context);
                var fmHouseTypes = GenerateManyFmHouseTypes(5);
                fmHouseTypes.ForEach(async fmHouseType => await service.CreateAsync(fmHouseType));
            }

            await using (var context = new ApplicationDbContext(options))
            {
                var service = new FmHouseTypeService(context);
                var fmHouseType = await service.GetFmHouseTypeByTypeAsync(1);
                var updatedHouseType = new FmHouseType
                {
                    Id = fmHouseType.Id,
                    HouseType = 2
                };
                var success = await service.UpdateAsync(updatedHouseType);
                Assert.True(success);
                Assert.Equal(fmHouseType.Id, updatedHouseType.Id);
                Assert.Equal(2, updatedHouseType.HouseType);
            }
        }
    }
}
