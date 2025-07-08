using Microsoft.AspNetCore.Mvc;
using FMA.DAL.Context;
using FMA.Common.DTOs;
using System.Linq;
using FMA.DAL.Entities;


namespace FMA.API.Controllers;
[ApiController]
[Route("[controller]")]
public class TeamController : ControllerBase
{
    private readonly FootballMatchAppContext _context;
    public TeamController(FootballMatchAppContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetTeams()
    {
        var teams = _context.Teams.Select(t => new TeamListDTO
        {
            TeamId = t.TeamId,
            TeamName = t.TeamName,
            CreatedByUserId = t.CreatedBy,
            Description = t.Description,
            ImageUrl = string.Empty, // FE sẽ cập nhật
            CreatedAt = null, // Chưa có cột CreatedAt
            UpdatedAt = null // Chưa có cột UpdatedAt
        }).ToList();

        return Ok(new
        {
            statusCode = 200,
            message = "Teams fetched successfully",
            data = teams
        });
    }

    [HttpGet("{id}")]
    public IActionResult GetTeamById(int id)
    {
        var team = _context.Teams.FirstOrDefault(t => t.TeamId == id);
        if (team == null) return NotFound();
        var dto = new TeamListDTO
        {
            TeamId = team.TeamId,
            TeamName = team.TeamName,
            CreatedByUserId = team.CreatedBy,
            Description = team.Description,
            ImageUrl = string.Empty,
            CreatedAt = null,
            UpdatedAt = null
        };
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult CreateTeam([FromBody] CreateTeamDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.TeamName))
        {
            return BadRequest(new { message = "TeamName is required" });
        }
        var team = new Team
        {
            TeamName = dto.TeamName,
            Description = dto.Description ?? string.Empty,
            CreatedBy = 1 // Hoặc lấy từ token nếu có auth
        };
        _context.Teams.Add(team);
        _context.SaveChanges();
        return Ok(new
        {
            statusCode = 201,
            message = "Team created successfully",
            data = new {
                team.TeamId,
                team.TeamName,
                team.CreatedBy,
                team.Description
            }
        });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTeam(int id, [FromBody] UpdateTeamDTO dto)
    {
        var team = _context.Teams.FirstOrDefault(t => t.TeamId == id);
        if (team == null) return NotFound();
        team.TeamName = dto.TeamName;
        team.Description = dto.Description;
        _context.SaveChanges();
        return Ok(new {
            statusCode = 200,
            message = "Team updated successfully",
            data = new {
                team.TeamId,
                team.TeamName,
                team.CreatedBy,
                team.Description
            }
        });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTeam(int id)
    {
        var team = _context.Teams.FirstOrDefault(t => t.TeamId == id);
        if (team == null)
        {
            return NotFound(new { statusCode = 404, message = "Team not found" });
        }
        _context.Teams.Remove(team);
        _context.SaveChanges();
        return Ok(new { statusCode = 200, message = "Team deleted successfully" });
    }
}