using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using YamhilliaNET.Constants;
using YamhilliaNET.Models;
using YamhilliaNET.Services;

namespace YamhilliaNETTests
{
    public class AnimalServiceTestCase: IntegrationTestCase
    {
        private readonly AnimalService service;

        public AnimalServiceTestCase()
        {
            this.service = (AnimalService)GetService<IAnimalService>();
        }

        [Fact]
        public async void TestCreate()
        {
            var user = await CreateUser();
            var farm = await GetDefaultFarm();
            var animal = service.Create(new Animal()
            {
                Name="Animal",
                Species = Species.Goat,
                Gender = Genders.Female,
                DateOfBirth = DateTime.Parse("2000/01/01"),
                CustomIdentifier = "42"
            }, farm, user);
            var createdCheck = await GetApplicationDbContext().Animals.Where(a => a.Id == animal.Id).Include(a => a.Farm).FirstOrDefaultAsync();
            Assert.NotNull(createdCheck);
            Assert.NotNull(createdCheck.Farm);
        }

        [Fact]
        public async void TestGetAccessibleAnimals_DefaultFarm()
        {
            var user = await CreateUser();
            var secondFarm = await CreateFarm();
            var user2 = await CreateUser(secondFarm);
            var farm = await GetDefaultFarm();
            var animal = service.Create(new Animal()
            {
                Name="Animal",
                Species = Species.Goat,
                Gender = Genders.Female,
                DateOfBirth = DateTime.Parse("2000/01/01"),
                CustomIdentifier = "42"
            }, farm, user);

            var animal2 = service.Create(new Animal()
            {
                Name="Animal2",
                Species = Species.Goat,
                Gender = Genders.Female,
                DateOfBirth = DateTime.Parse("2000/01/01"),
                CustomIdentifier = "404"
            }, farm, user);

            var animal3 = service.Create(new Animal()
            {
                Name="Animal4",
                Species = Species.Goat,
                Gender = Genders.Female,
                DateOfBirth = DateTime.Parse("2000/01/01"),
                CustomIdentifier = "69"
            }, secondFarm, user2);

            var animals = await service.GetAccessibleAnimals(user);
            Assert.Equal(2, animals.Count());
            var animals2 = await service.GetAccessibleAnimals(user2);
            Assert.Single(animals2);
        }
    }
}