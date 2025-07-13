using FMA.BLL.Services.Interfaces;
using FMA.Common.DTOs;
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
        /// <summary>
        /// update
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserDTO updateUserDTO)
        {
           
            var response = await _userService.UpdateProfileAsync(updateUserDTO);
            return StatusCode(response.StatusCode, response);
        }
    }
}
