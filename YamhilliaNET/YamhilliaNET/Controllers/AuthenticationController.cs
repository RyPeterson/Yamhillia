using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YamhilliaNET.Models;
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

        private readonly UserManager<YamhilliaUser> _userManager;

        public AuthenticationController(Services.Auth.IAuthenticationService authService, IUserService userService, UserManager<YamhilliaUser> userManager)
        {
            this.authService = authService;
            this.userService = userService;
            _userManager = userManager;
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
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try 
            {
                var userAndToken = await authService.Login(loginModel);
                return Ok(new { User = userAndToken });
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

        [HttpGet]
        [Route("user")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUser()
        {
            var contextUser = User;
            
            if(contextUser != null)
            {
                var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if(!string.IsNullOrEmpty(id))
                {
                    var yamhilliaUser = await userService.GetUserById(id);
                    if(yamhilliaUser != null)
                    {
                        yamhilliaUser.PasswordHash = null;
                        return Ok(new {user =  yamhilliaUser});
                    }

                }
            }

            return Unauthorized();
        }
    }
}