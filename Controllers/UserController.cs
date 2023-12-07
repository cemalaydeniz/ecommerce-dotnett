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

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
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
    }
}
