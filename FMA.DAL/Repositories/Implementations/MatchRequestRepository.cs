using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.DAL.Context;
using FMA.DAL.Entities;
using FMA.DAL.Repositories.Interfaces;

namespace FMA.DAL.Repositories.Implementations
{
    public class MatchRequestRepository : GenericRepository<MatchRequest>, IMatchRequestRepository
    {
        private readonly FootballMatchAppContext _context;
        public MatchRequestRepository(FootballMatchAppContext context) : base(context)
        {
            _context = context;
        }
    }
}
