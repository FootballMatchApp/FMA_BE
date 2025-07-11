using FMA.BLL.Services.Interfaces;
using FMA.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FMA.API.Controllers
{
    [Route("api/[controller]")]
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
            var result = await _matchPostService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }
    }

    }
