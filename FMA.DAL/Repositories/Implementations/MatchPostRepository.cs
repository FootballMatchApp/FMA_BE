using FMA.DAL.Context;
using FMA.DAL.Entities;
using FMA.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FMA.DAL.Repositories.Implementations;

public class MatchPostRepository : GenericRepository<MatchPost>, IMatchPostRepository
{
    private readonly FootballMatchAppContext _context;
    public MatchPostRepository(FootballMatchAppContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<MatchPost>> GetAllAsync()
    {
        return await _context.MatchPosts.ToListAsync();
    }

}