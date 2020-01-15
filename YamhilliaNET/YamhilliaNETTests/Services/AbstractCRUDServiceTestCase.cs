using System.Linq;
using Xunit;
using YamhilliaNET.Models;
using YamhilliaNET.Services;

namespace YamhilliaNETTests.Services
{
    public class AbstractCRUDServiceTestCase : IntegrationTestCase
    {
        private readonly AbstractCRUDService<Farm> service;

        public AbstractCRUDServiceTestCase()
        {
            service = (FarmService)GetService<IFarmService>();
        }

        [Fact]
        public async void TestCreate()
        {
            var farm = await service.Create(new Farm() {Name= "Test"});
            Assert.True(farm.Id > 1);
            Assert.NotNull(farm.CreatedAt);
            Assert.NotNull(farm.UpdatedAt);
        }

        [Fact]
        public async void TestUpdate()
        {
            var farm = await service.Create(new Farm() {Name= "Test"});
            farm.Name = "Test2";
            var updated = await service.Update(farm);
            Assert.Equal("Test2", updated.Name);
        }

        [Fact]
        public async void TestGet()
        {
            var farm = await service.Create(new Farm() {Name= "Test"});
            var retrieved = await service.Get(farm.Id);
            Assert.Equal(farm.Id, retrieved.Id);
            Assert.Equal(farm.Name, retrieved.Name);
        }

        [Fact]
        public async void TestDelete()
        {
            var farm = await service.Create(new Farm() {Name= "Test"});
            await service.Delete(farm.Id);
            var retrieved = await service.Get(farm.Id);
            Assert.Null(retrieved);
        }

        [Fact]
        public async void TestGet_List()
        {
            await service.Create(new Farm() {Name= "Test"});
            await service.Create(new Farm() {Name= "Test"});
            await service.Create(new Farm() {Name= "Test"});
            var list = (await service.Get(new GetOptions())).ToList();
            // + seeded default
            Assert.Equal(4, list.Count);

            var list2 = (await service.Get(new GetOptions() { Limit = 2})).ToList();
            Assert.Equal(2, list2.Count);
        }
        
        
    }
}