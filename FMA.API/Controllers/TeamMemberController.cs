using FMA.BLL.Services.Implementations;
using FMA.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FMA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        private readonly ITeamMemberService _teamMemberService;

        public TeamMemberController(ITeamMemberService teamMemberService)
        {
            _teamMemberService = teamMemberService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamMembers(Guid id)
        {
            var response = await _teamMemberService.GetTeamMembersAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("detail/{userId}")]
        public async Task<IActionResult> GetTeamMemberDetail(Guid userId)
        {
            var response = await _teamMemberService.GetTeamMemberDetailAsync(userId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTeamMembers()
        {
            var response = await _teamMemberService.GetAllTeamMember();
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddMemberToTeam(Guid teamId, Guid userId, string? position)
        {
            var response = await _teamMemberService.AddMemberToTeamAsync(teamId, userId, position);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveMemberFromTeam(Guid teamId, Guid userId)
        {
            var response = await _teamMemberService.RemoveMemberFromTeamAsync(teamId, userId);
            return StatusCode(response.StatusCode, response);

        }
    }
}
