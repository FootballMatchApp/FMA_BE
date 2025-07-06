using FMA.DAL.Context;
using FMA.DAL.Entities;
using FMA.DAL.Repositories.Interfaces;

namespace FMA.DAL.Repositories.Implementations;

public class TeamRepository : GenericRepository<Team>, ITeamRepository
{
    private readonly FootballMatchAppContext _context;
    public TeamRepository(FootballMatchAppContext context) : base(context)
    {
        _context = context;
    }
}