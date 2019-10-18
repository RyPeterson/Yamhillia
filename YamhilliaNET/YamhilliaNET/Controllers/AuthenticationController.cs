using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamhilliaNET.Services.Auth;
using YamhilliaNET.Services.User;

namespace YamhilliaNET.Controllers
{
    [ApiController]
    [Route("/api/yamhillia/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly Services.Auth.IAuthenticationService authService;
        private readonly IUserService userService;

        public AuthenticationController(Services.Auth.IAuthenticationService authService, IUserService userService)
        {
            this.authService = authService;
            this.userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create-token")]
        public async Task<IActionResult> CreateToken([FromBody]LoginModel loginModel)
        {
            try 
            {
                var token = await authService.CreateToken(loginModel);
                return Ok(new { token = token });
            }
            catch(InvalidUserNameOrPasswordException)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]CreateUserModel createUserModel)
        {
            try 
            {
                var user = await userService.Create(createUserModel);
                var token = await authService.CreateToken(new LoginModel{Username = user.Email, Password = createUserModel.Password});
                return Ok(new { token = token });
            }
            catch(InvalidUserNameOrPasswordException)
            {
                return BadRequest();
            }
        }
    }
}