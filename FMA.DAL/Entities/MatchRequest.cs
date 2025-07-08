using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.Common.Enums;

namespace FMA.DAL.Entities
{
    public class MatchRequest
    {
        public int MatchRequestId { get; set; }
        public int MatchPostId { get; set; }
        public int RequestById { get; set; }
        public int? RequestByTeamId { get; set; }
        public virtual Team? RequestByTeam { get; set; }
        public MatchRequestStatus Status { get; set; } // Pending, Accepted, Rejected
        public DateTime RequestTime { get; set; }
        public DateTime? DecisionTime { get; set; }
        public virtual MatchPost MatchPost { get; set; } = null!;
        public virtual User RequestBy { get; set; } = null!;
    }

}
