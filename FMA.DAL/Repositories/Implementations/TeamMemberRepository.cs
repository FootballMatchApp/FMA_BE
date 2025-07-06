using FMA.DAL.Context;
using FMA.DAL.Entities;
using FMA.DAL.Repositories.Interfaces;

namespace FMA.DAL.Repositories.Implementations;

public class TeamMemberRepository : GenericRepository<TeamMember>, ITeamMemberRepository
{
    private readonly FootballMatchAppContext _context;
    public TeamMemberRepository (FootballMatchAppContext context) : base(context)
    {
        _context = context;
    }
}