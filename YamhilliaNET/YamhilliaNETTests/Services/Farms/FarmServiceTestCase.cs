using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using YamhilliaNET.Exceptions;
using YamhilliaNET.Models.Farms;
using YamhilliaNET.Services.Farms;
using YamhilliaNET.Services.Users;

namespace YamhilliaNETTests.Services.Farms
{
    public class FarmServiceTestCase: IntegrationTestCase
    {
        private readonly FarmService _farmService;
        public FarmServiceTestCase()
        {
            _farmService = new FarmService(GetDbContext(), GetService<IUserService>());
        }

        [Fact]
        public async void Test_CreateFarm_HappyRoute()
        {
            var owner = await CreateTestUser();
            var farm = await _farmService.CreateFarm(owner.Id, new CreateFarmParams() { Name = "Test"});
            Assert.NotNull(farm);
            var fromDb = await GetDbContext().Farms.Where(f => f.OwnerId == owner.Id).FirstAsync();
            Assert.Equal(fromDb.OwnerId, owner.Id);
        }

        [Fact]
        public async void Test_CreatFarm_UserAlreadyCreated()
        {
            var owner = await CreateTestUser();
            await CreateTestFarm(owner);
            await Assert.ThrowsAsync<YamhilliaBadRequestError>(() =>
                _farmService.CreateFarm(owner.Id, new CreateFarmParams() {Name = "Test"}));
        }

        [Fact]
        public async void Test_GetFarmByOwner()
        {
            var owner = await CreateTestUser();
            Assert.Null(await _farmService.GetFarmByOwner(owner.Id));
            var farm = await CreateTestFarm(owner);
            var farmByUser = await _farmService.GetFarmByOwner(owner.Id);
            Assert.Equal(farm.Id, farmByUser.Id);
        }
    }
}