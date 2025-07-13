using FMA.BLL.Services.Interfaces;
using FMA.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FMA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchRequestController : ControllerBase
    {
        private readonly IMatchRequestService _matchRequestService;

        public MatchRequestController(IMatchRequestService matchRequestService)
        {
            _matchRequestService = matchRequestService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMatchRequest([FromBody] CreateMatchRequestDTO createMatchRequestDTO)
        {
            var response = await _matchRequestService.CreateMatchRequestAsync(createMatchRequestDTO);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateMatchRequest([FromBody] UpdateMatchRequestDTO updateMatchRequestDTO)
        {
            var response = await _matchRequestService.UpdateMatchRequestAsync(updateMatchRequestDTO);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("delete/{matchRequestId}")]
        public async Task<IActionResult> DeleteMatchRequest(Guid matchRequestId)
        {
            var response = await _matchRequestService.DeleteMatchRequestAsync(matchRequestId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("get/{matchRequestId}")]
        public async Task<IActionResult> GetMatchRequestById(Guid matchRequestId)
        {
            var response = await _matchRequestService.GetMatchRequestByIdAsync(matchRequestId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("getByMatchPostId/{matchPostId}")]
        public async Task<IActionResult> GetMatchRequestsByMatchPostId(Guid matchPostId)
        {
            var response = await _matchRequestService.GetMatchRequestsByMatchPostIdAsync(matchPostId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("getByUserId/{userId}")]
        public async Task<IActionResult> GetMatchRequestsByUserId(Guid userId)
        {
            var response = await _matchRequestService.GetMatchRequestsByUserIdAsync(userId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("getByTeamId/{teamId}")]
        public async Task<IActionResult> GetMatchRequestsByTeamId(Guid teamId)
        {
            var response = await _matchRequestService.GetMatchRequestsByTeamIdAsync(teamId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllMatchRequests()
        {
            var response = await _matchRequestService.GetAllMatchRequestsAsync();
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("accept/{matchRequestId}")]
        public async Task<IActionResult> AcceptMatchRequest(Guid matchRequestId)
        {
            var response = await _matchRequestService.AcceptMatchRequestAsync(matchRequestId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("cancel/{matchRequestId}")]
        public async Task<IActionResult> CancelMatchRequest(Guid matchRequestId)
        {
            var response = await _matchRequestService.CancelMatchRequestAsync(matchRequestId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("getByStatus/{status}")]
        public async Task<IActionResult> GetMatchRequestsByStatus(string status)
        {
            var response = await _matchRequestService.GetMatchRequestsByStatusAsync(status);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("getByUserId")]
        public async Task<IActionResult> GetMatchRequestsByUserId()
        {
            var response = await _matchRequestService.GetMatchRequestsByUserId();
            return StatusCode(response.StatusCode, response);
        }
    }

}
