using FMA.DAL.Context;
using FMA.DAL.Entities;
using FMA.DAL.Repositories.Interfaces;

namespace FMA.DAL.Repositories.Implementations;

public class MatchPostRepository : GenericRepository<MatchPost>, IMatchPostRepository
{
    private new readonly FootballMatchAppContext _context;
    public MatchPostRepository(FootballMatchAppContext context) : base(context)
    {
        _context = context;
    }
}