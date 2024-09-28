using Microsoft.AspNetCore.Mvc;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.API.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var res = await _userService.GetUsers();
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto request)
        {
            var results = await _userService.CreateUser(request);
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserWallet(Guid id)
        {
            var user = await _userService.GetUserById(id);
            var res = await _userService.GetUserWallet(id);
            return Ok(new { user = user, account = res.Wallets});
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var userId = HttpContext.Items["userId"]!.ToString();

            if (userId == null) return Unauthorized(new { message = "Unauthorized" });

            var user = await _userService.GetUserById(Guid.Parse(userId));
            return Ok(new { user = user});
        }
    }
}
