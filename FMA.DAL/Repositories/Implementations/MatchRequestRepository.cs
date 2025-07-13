using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.Common.Enums;
using FMA.DAL.Context;
using FMA.DAL.Entities;
using FMA.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FMA.DAL.Repositories.Implementations
{
    public class MatchRequestRepository : GenericRepository<MatchRequest>, IMatchRequestRepository
    {
        private readonly FootballMatchAppContext _context;
        public MatchRequestRepository(FootballMatchAppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<MatchRequest>> GetByMatchPostIdAsync(Guid matchPostId)
        {
            return await _context.MatchRequests
                .Where(mr => mr.MatchPostId == matchPostId)
                .Include(mr => mr.MatchPost)
                .Include(mr => mr.RequestBy)
                .Include(mr => mr.RequestByTeam)
                .ToListAsync();
        }

        public async Task<List<MatchRequest>> GetByStatusAsync(MatchRequestStatus status)
        {
            return await _context.MatchRequests
                .Where(mr => mr.Status == status)
                .Include(mr => mr.MatchPost)
                .Include(mr => mr.RequestBy)
                .Include(mr => mr.RequestByTeam)
                .ToListAsync();
        }

        public async Task<List<MatchRequest>> GetByTeamIdAsync(Guid teamId)
        {
            return await _context.MatchRequests
                .Where(mr => mr.RequestByTeamId.HasValue && mr.RequestByTeamId.Value == teamId)
                .Include(mr => mr.MatchPost)
                .Include(mr => mr.RequestBy)
                .Include(mr => mr.RequestByTeam)
                .ToListAsync();
        }

        public async Task<List<MatchRequest>> GetByUserIdAsync(Guid userId)
        {
            return await _context.MatchRequests
                .Where(mr => mr.RequestById == userId)
                .Include(mr => mr.MatchPost)
                .Include(mr => mr.RequestBy)
                .Include(mr => mr.RequestByTeam)
                .ToListAsync();
        }
    }
}
