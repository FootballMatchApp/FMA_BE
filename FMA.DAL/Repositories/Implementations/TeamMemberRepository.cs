using FMA.DAL.Context;
using FMA.DAL.Entities;
using FMA.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FMA.DAL.Repositories.Implementations;

public class TeamMemberRepository : GenericRepository<TeamMember>, ITeamMemberRepository
{
    private readonly FootballMatchAppContext _context;
    public TeamMemberRepository (FootballMatchAppContext context) : base(context)
    {
        _context = context;
    }

    public async Task<TeamMember> GetByTeamAndUserAsync(Guid teamId, Guid userId)
    {
        return await _context.TeamMembers
            .FirstOrDefaultAsync(tm => tm.TeamId == teamId && tm.UserId == userId);
    }

    public async Task<TeamMember?> GetTeamMemberDetailAsync(Guid userId)
    {
        return await _context.TeamMembers
            .Include(tm => tm.User)
            .Include(tm => tm.Team)
            .FirstOrDefaultAsync(tm => tm.UserId == userId);
    }

    public async Task<IEnumerable<TeamMember>> GetMembersByTeamIdAsync(Guid teamId)
    {
        return await _context.TeamMembers
            .Where(tm => tm.TeamId == teamId)
            .Include(tm => tm.User)

            .ToListAsync();
    }
}