using Microsoft.AspNetCore.Mvc;
using FMA.DAL.Context;
using FMA.Common.DTOs;
using System.Linq;
using FMA.DAL.Entities;

namespace FMA.API.Controllers;
[ApiController]
[Route("[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly FootballMatchAppContext _context;
    public ProfilesController(FootballMatchAppContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetProfiles()
    {
        var profiles = (from p in _context.PlayerProfiles
                        join u in _context.Users on p.UserId equals u.UserId into userJoin
                        from u in userJoin.DefaultIfEmpty()
                        join tm in _context.TeamMembers on p.PlayerId equals tm.PlayerId into teamJoin
                        from tm in teamJoin.DefaultIfEmpty()
                        select new ProfileDTO
                        {
                            Id = p.PlayerId,
                            Name = p.FullName,
                            TeamId = tm != null ? (int?)tm.TeamId : null,
                            Email = u != null ? u.Email : string.Empty,
                            Avatar = p.Avatar,
                            Phone = p.PhoneNumber,
                            Age = p.Age,
                            Position = p.Position,
                            Bio = p.Bio
                        }).ToList();

        return Ok(new
        {
            success = true,
            message = "Profiles retrieved successfully",
            data = profiles
        });
    }

    [HttpGet("{id}")]
    public IActionResult GetProfileById(int id)
    {
        var profile = _context.PlayerProfiles.FirstOrDefault(p => p.PlayerId == id);
        if (profile == null) return NotFound();
        var dto = new ProfileDTO
        {
            Id = profile.PlayerId,
            Name = profile.FullName,
            TeamId = null,
            Email = string.Empty,
            Avatar = profile.Avatar,
            Phone = profile.PhoneNumber,
            Age = profile.Age,
            Position = profile.Position,
            Bio = profile.Bio
        };
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult CreateProfile([FromBody] ProfileDTO dto)
    {
        var profile = new PlayerProfile
        {
            FullName = dto.Name,
            PhoneNumber = dto.Phone,
            Position = dto.Position,
            UserId = 1, // hoặc lấy từ token nếu có auth
            Bio = dto.Bio,
            Age = dto.Age,
            Avatar = dto.Avatar
        };
        _context.PlayerProfiles.Add(profile);
        _context.SaveChanges();
        return Ok(profile);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProfile(int id, [FromBody] ProfileDTO dto)
    {
        var profile = _context.PlayerProfiles.FirstOrDefault(p => p.PlayerId == id);
        if (profile == null) return NotFound();
        profile.FullName = dto.Name;
        profile.PhoneNumber = dto.Phone;
        profile.Position = dto.Position;
        profile.Bio = dto.Bio;
        profile.Age = dto.Age;
        profile.Avatar = dto.Avatar;
        _context.SaveChanges();
        return Ok(profile);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProfile(int id)
    {
        var profile = _context.PlayerProfiles.FirstOrDefault(p => p.PlayerId == id);
        if (profile == null)
        {
            return NotFound(new { statusCode = 404, message = "Profile not found" });
        }
        _context.PlayerProfiles.Remove(profile);
        _context.SaveChanges();
        return Ok(new { statusCode = 200, message = "Profile deleted successfully" });
    }
}
