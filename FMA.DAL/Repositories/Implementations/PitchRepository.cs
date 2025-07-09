using FMA.DAL.Context;
using FMA.DAL.Entities;
using FMA.DAL.Repositories.Interfaces;

namespace FMA.DAL.Repositories.Implementations;

public class PitchRepository : GenericRepository<Pitch>, IPitchRepository
{
    private new readonly FootballMatchAppContext _context;
    public PitchRepository(FootballMatchAppContext context) : base(context)
    {
        _context = context;
    }
}