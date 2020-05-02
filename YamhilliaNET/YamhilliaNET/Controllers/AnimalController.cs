using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamhilliaNET.Models;
using YamhilliaNET.Services;
using YamhilliaNET.Services.Auth;

namespace YamhilliaNET.Controllers
{
    [ApiController]
    [Route("/api/yamhillia/animals")]
    public class AnimalController: ControllerBase
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IAnimalService animalService;
        private readonly IFarmService farmService;

        public AnimalController(IAuthenticationService authenticationService, IAnimalService animalService, IFarmService farmService)
        {
            this.authenticationService = authenticationService;
            this.animalService = animalService;
            this.farmService = farmService;
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("create")]
        public async Task<ActionResult<Animal>> Create([FromBody]Animal animal)
        {
            var user = await authenticationService.GetUserFromContext(User);
            var farm = await farmService.Get(user.FarmId);
            var createdAnimal = await animalService.Create(animal, farm, user);
            return Ok(new {animal =  createdAnimal});
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<Animal>>> Get()
        {
            var user = await authenticationService.GetUserFromContext(User);
            var animals = await animalService.GetAccessibleAnimals(user);
            return Ok(new {animalList =  animals});
        }
    }
}