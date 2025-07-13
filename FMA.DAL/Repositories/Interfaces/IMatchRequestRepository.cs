using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.Common.Enums;
using FMA.DAL.Entities;

namespace FMA.DAL.Repositories.Interfaces
{
    public interface IMatchRequestRepository : IGenericRepository<MatchRequest>
    {
        Task<List<MatchRequest>> GetByMatchPostIdAsync(Guid matchPostId);
        Task<List<MatchRequest>> GetByStatusAsync(MatchRequestStatus status);
        Task<List<MatchRequest>> GetByTeamIdAsync(Guid teamId);
        Task<List<MatchRequest>> GetByUserIdAsync(Guid userId); 
    }
}
