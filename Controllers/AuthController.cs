using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuoteAPI.Entities;
using QuoteAPI.Models;
using QuoteAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuoteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : Controller
    {
        public static User user = new User();

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(UserDTO request)
        {
            var user = await authService.RegisterAsync(request);
            if (user == null)
            {
                return BadRequest("Username is already taken");
            }
            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDTO request)
        {
            var token = await authService.LoginAsync(request);
            if (token == null)
            {
                return BadRequest("Invalid username or password");
            } 
            return Ok(token);
        }

        [Authorize] 
        [HttpGet]
        public IActionResult AuthenicatedOnlyEndpoint()
        {
            return Ok("You are authenticated");
        }
    }
}
