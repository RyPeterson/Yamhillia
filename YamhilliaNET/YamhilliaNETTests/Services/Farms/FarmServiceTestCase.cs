using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using YamhilliaNET.Constants;
using YamhilliaNET.Exceptions;
using YamhilliaNET.Models.Farms;
using YamhilliaNET.Services.Farms;
using YamhilliaNET.Services.Users;

namespace YamhilliaNETTests.Services.Farms
{
    public class FarmServiceTestCase : IntegrationTestCase
    {
        private readonly FarmService _farmService;

        public FarmServiceTestCase()
        {
            _farmService = new FarmService(GetDbContext(), GetService<IUserService>(),
                GetService<ILogger<FarmService>>());
        }

        [Fact]
        public async void Test_CreateFarm_HappyRoute()
        {
            var owner = await CreateTestUser();
            var farm = await _farmService.CreateFarm(owner.Id, new CreateFarmParams {Name = "Test"});
            Assert.NotNull(farm);
            var ownership = await GetDbContext().FarmMemberships.Where(m => m.FarmId == farm.Id).ToListAsync();
            Assert.NotEmpty(ownership);
            Assert.Equal(ownership[0].UserId, owner.Id);
            Assert.Equal(MemberType.OWNER, ownership[0].MemberType);
        }

        [Fact]
        public async void Test_CreatFarm_UserAlreadyCreated()
        {
            var owner = await CreateTestUser();
            await CreateTestFarm(owner);
            await Assert.ThrowsAsync<YamhilliaBadRequestError>(() =>
                _farmService.CreateFarm(owner.Id, new CreateFarmParams {Name = "Test"}));
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

        [Fact]
        public async void Test_AddUserToFarm_OwnerOwner()
        {
            var owner = await CreateTestUser();
            var farm = await CreateTestFarm(owner);

            var otherOwner = await CreateTestUser();
            await _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = owner.Id,
                FarmId = farm.Id,
                UserId = otherOwner.Id,
                MemberType = MemberType.OWNER
            });

            var result = await GetDbContext().FarmMemberships.Where(m => m.UserId == otherOwner.Id)
                .FirstOrDefaultAsync();
            Assert.NotNull(result);
            Assert.Equal(MemberType.OWNER, result.MemberType);
        }

        [Fact]
        public async void Test_AddUserToFarm_OwnerAdmin()
        {
            var owner = await CreateTestUser();
            var farm = await CreateTestFarm(owner);

            var admin = await CreateTestUser();
            await _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = owner.Id,
                FarmId = farm.Id,
                UserId = admin.Id,
                MemberType = MemberType.ADMINISTRATOR
            });

            var result = await GetDbContext().FarmMemberships.Where(m => m.UserId == admin.Id)
                .FirstOrDefaultAsync();
            Assert.NotNull(result);
            Assert.Equal(MemberType.ADMINISTRATOR, result.MemberType);
        }

        [Fact]
        public async void Test_AddUserToFarm_OwnerWorker()
        {
            var owner = await CreateTestUser();
            var farm = await CreateTestFarm(owner);

            var worker = await CreateTestUser();
            await _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = owner.Id,
                FarmId = farm.Id,
                UserId = worker.Id,
                MemberType = MemberType.WORKER
            });

            var result = await GetDbContext().FarmMemberships.Where(m => m.UserId == worker.Id)
                .FirstOrDefaultAsync();
            Assert.NotNull(result);
            Assert.Equal(MemberType.WORKER, result.MemberType);
        }

        [Fact]
        public async void Test_AddUserToFarm_OwnerGuest()
        {
            var owner = await CreateTestUser();
            var farm = await CreateTestFarm(owner);

            var guest = await CreateTestUser();
            await _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = owner.Id,
                FarmId = farm.Id,
                UserId = guest.Id,
                MemberType = MemberType.GUEST
            });

            var result = await GetDbContext().FarmMemberships.Where(m => m.UserId == guest.Id)
                .FirstOrDefaultAsync();
            Assert.NotNull(result);
            Assert.Equal(MemberType.GUEST, result.MemberType);
        }

        [Fact]
        public async void Test_AddUserToFarm_AdminWorker()
        {
            var owner = await CreateTestUser();
            var farm = await CreateTestFarm(owner);

            var admin = await CreateTestUser();
            var worker = await CreateTestUser();
            await _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = owner.Id,
                FarmId = farm.Id,
                UserId = admin.Id,
                MemberType = MemberType.ADMINISTRATOR
            });

            await _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = admin.Id,
                FarmId = farm.Id,
                UserId = worker.Id,
                MemberType = MemberType.WORKER
            });

            var result = await GetDbContext().FarmMemberships.Where(m => m.UserId == worker.Id)
                .FirstOrDefaultAsync();
            Assert.NotNull(result);
            Assert.Equal(MemberType.WORKER, result.MemberType);
        }


        [Fact]
        public async void Test_AddUserToFarm_AdminGuest()
        {
            var owner = await CreateTestUser();
            var farm = await CreateTestFarm(owner);

            var admin = await CreateTestUser();
            var guest = await CreateTestUser();
            await _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = owner.Id,
                FarmId = farm.Id,
                UserId = admin.Id,
                MemberType = MemberType.ADMINISTRATOR
            });

            await _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = admin.Id,
                FarmId = farm.Id,
                UserId = guest.Id,
                MemberType = MemberType.GUEST
            });

            var result = await GetDbContext().FarmMemberships.Where(m => m.UserId == guest.Id)
                .FirstOrDefaultAsync();
            Assert.NotNull(result);
            Assert.Equal(MemberType.GUEST, result.MemberType);
        }


        [Fact]
        public async void Test_AddUserToFarm_AdminAdmin()
        {
            var owner = await CreateTestUser();
            var farm = await CreateTestFarm(owner);

            var admin = await CreateTestUser();
            var otherAdmin = await CreateTestUser();
            await _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = owner.Id,
                FarmId = farm.Id,
                UserId = admin.Id,
                MemberType = MemberType.ADMINISTRATOR
            });

            await Assert.ThrowsAsync<YamhilliaBadRequestError>(() => _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = admin.Id,
                FarmId = farm.Id,
                UserId = otherAdmin.Id,
                MemberType = MemberType.ADMINISTRATOR
            }));
        }


        [Fact]
        public async void Test_AddUserToFarm_AdminOwner()
        {
            var owner = await CreateTestUser();
            var farm = await CreateTestFarm(owner);

            var admin = await CreateTestUser();
            var ownerToAdd = await CreateTestUser();
            await _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = owner.Id,
                FarmId = farm.Id,
                UserId = admin.Id,
                MemberType = MemberType.ADMINISTRATOR
            });

            await Assert.ThrowsAsync<YamhilliaBadRequestError>(() => _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = admin.Id,
                FarmId = farm.Id,
                UserId = ownerToAdd.Id,
                MemberType = MemberType.ADMINISTRATOR
            }));
        }

        [Fact]
        public async void Test_AddUserToFarm_WorkerAny()
        {
            var owner = await CreateTestUser();
            var farm = await CreateTestFarm(owner);

            var worker = await CreateTestUser();
            var toAdd = await CreateTestUser();
            await _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = owner.Id,
                FarmId = farm.Id,
                UserId = worker.Id,
                MemberType = MemberType.WORKER
            });

            await Assert.ThrowsAsync<YamhilliaBadRequestError>(() => _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = worker.Id,
                FarmId = farm.Id,
                UserId = toAdd.Id,
                MemberType = MemberType.WORKER
            }));
        }


        [Fact]
        public async void Test_AddUserToFarm_GuestAny()
        {
            var owner = await CreateTestUser();
            var farm = await CreateTestFarm(owner);

            var guest = await CreateTestUser();
            var toAdd = await CreateTestUser();
            await _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = owner.Id,
                FarmId = farm.Id,
                UserId = guest.Id,
                MemberType = MemberType.GUEST
            });

            await Assert.ThrowsAsync<YamhilliaBadRequestError>(() => _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = guest.Id,
                FarmId = farm.Id,
                UserId = toAdd.Id,
                MemberType = MemberType.GUEST
            }));
        }

        [Fact]
        public async void Test_AddUserToFarm_ExistsAlready()
        {
            var owner = await CreateTestUser();
            var farm = await CreateTestFarm(owner);
            var toAdd = await CreateTestUser();

            await _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = owner.Id,
                FarmId = farm.Id,
                UserId = toAdd.Id,
                MemberType = MemberType.WORKER
            });


            await _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = owner.Id,
                FarmId = farm.Id,
                UserId = toAdd.Id,
                MemberType = MemberType.WORKER
            });

            var count = await GetDbContext().FarmMemberships.Where(m => m.FarmId == farm.Id && m.UserId == toAdd.Id)
                .CountAsync();

            Assert.Equal(1, count);
        }

        [Fact]
        public async void Test_AddUserToFarm_ExistsInDifferentFarm()
        {
            var owner = await CreateTestUser();
            var farm = await CreateTestFarm(owner);
            var toAdd = await CreateTestUser();
            await CreateTestFarm(toAdd);

            await _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = owner.Id,
                FarmId = farm.Id,
                UserId = toAdd.Id,
                MemberType = MemberType.WORKER
            });

            var count = await GetDbContext().FarmMemberships.Where(m => m.FarmId == farm.Id && m.UserId == toAdd.Id)
                .CountAsync();

            Assert.Equal(1, count);

            var totalCount = await GetDbContext().FarmMemberships.Where(m => m.UserId == toAdd.Id)
                .CountAsync();

            Assert.Equal(2, totalCount);
        }

        [Fact]
        public async void Test_AddUserToFarm_ExistsInOtherFarm_RespectsCurrentFarmAccessLevel()
        {
            var owner = await CreateTestUser();
            var farm = await CreateTestFarm(owner);
            var toAdd = await CreateTestUser();
            await CreateTestFarm(toAdd);

            await _farmService.AddUserToFarm(new AddUserToFarmParams
            {
                RequesterId = owner.Id,
                FarmId = farm.Id,
                UserId = toAdd.Id,
                MemberType = MemberType.WORKER
            });

            var otherAdmin = await CreateTestUser();

            await Assert.ThrowsAsync<YamhilliaBadRequestError>(() => _farmService.AddUserToFarm(
                new AddUserToFarmParams
                {
                    RequesterId = toAdd.Id,
                    FarmId = farm.Id,
                    MemberType = MemberType.ADMINISTRATOR,
                    UserId = otherAdmin.Id
                }));
        }
    }
}