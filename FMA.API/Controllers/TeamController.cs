using FMA.BLL.Services.Interfaces;
using FMA.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FMA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetTeams()
        {
            var response = await _teamService.GetAllTeamsAsync();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamById(Guid id)
        {
            var response = await _teamService.GetTeamByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost()]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamDTO createTeamDto)
        {
            var response = await _teamService.CreateTeamAsync(createTeamDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut()]
        public async Task<IActionResult> UpdateTeam([FromBody] UpdateTeamDTO updateTeamDto)
        {
            var response = await _teamService.UpdateTeamAsync(updateTeamDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(Guid id)
        {
            var response = await _teamService.DeleteTeamAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}