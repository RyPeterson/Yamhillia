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
            var farm = await GetDefaultFarm();
            var animal = service.Create(new Animal()
            {
                Name="Animal",
                Species = Species.Goat,
                Gender = Genders.Female,
                DateOfBirth = DateTime.Parse("2000/01/01"),
                CustomIdentifier = "42"
            }, farm);
            var createdCheck = await GetApplicationDbContext().Animals.Where(a => a.Id == animal.Id).Include(a => a.Farm).FirstOrDefaultAsync();
            Assert.NotNull(createdCheck);
            Assert.NotNull(createdCheck.Farm);
        }
    }
}