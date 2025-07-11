using FMA.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FMA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// profile
        /// </summary>
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var response = await _userService.GetProfileAsync();
            return StatusCode(response.StatusCode, response);
        }
    }
}
