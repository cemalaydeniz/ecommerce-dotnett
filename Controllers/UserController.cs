using ecommerce_dotnet.DTOs.User;
using ecommerce_dotnet.Models;
using ecommerce_dotnet.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace ecommerce_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel registerModel)
        {
            if (registerModel == null)
                return BadRequest(JsonResponse.Error(Constants.Response.General.BadRequest));

            User newUser = new User()
            {
                Name = registerModel.Name,
                Email = registerModel.Email,
                UserName = registerModel.Email,
                PhoneNumber = registerModel.PhoneNumber,
                Address = registerModel.Address
            };

            var result = _userManager.CreateAsync(newUser, registerModel.Password).Result;
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(_ => _.Description);

                return BadRequest(JsonResponse.Error(string.Join(", ", errors)));
            }

            await _userManager.AddToRoleAsync(newUser, Constants.Roles.User);

            return StatusCode((int)HttpStatusCode.Created, JsonResponse.Success(Constants.Response.User.UserCreated));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginModel loginModel)
        {
            if (loginModel == null)
                return BadRequest(JsonResponse.Error(Constants.Response.General.BadRequest));

            User? user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null)
                return NotFound(JsonResponse.Error(Constants.Response.User.UserNotFound));

            // In case the user was already logged in
            await _signInManager.SignOutAsync();

            var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
            if (!result.Succeeded)
                return BadRequest(JsonResponse.Error(Constants.Response.User.WrongPassword));

            return Ok(JsonResponse.Success(Constants.Response.User.LoggedIn));
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok(JsonResponse.Success(Constants.Response.User.LoggedOut));
        }
    }
}
