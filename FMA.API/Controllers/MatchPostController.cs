using FMA.BLL.Services.Interfaces;
using FMA.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FMA.API.Controllers
{
    [Route("matchpost")]
    [ApiController]
    public class MatchPostController : ControllerBase
    {
        private readonly IMatchPostService _matchPostService;

        public MatchPostController(IMatchPostService matchPostService)
        {
            _matchPostService = matchPostService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _matchPostService.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }
    
    [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMatchPostDTO dto)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null) return Unauthorized("User not authenticated");

            var userId = Guid.Parse(userIdClaim.Value);

            var result = await _matchPostService.CreateAsync(dto, userId);
            return StatusCode(result.StatusCode, result);
        }
    }

    }
