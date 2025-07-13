using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.Common.Enums;

namespace FMA.Common.DTOs
{
    public class CreateMatchRequestDTO
    {
        public Guid MatchPostId { get; set; }
        public Guid? RequestByTeamId { get; set; }
    }

    public class UpdateMatchRequestDTO
    {
        public Guid MatchRequestId { get; set; }

        public Guid? MatchPost { get; set; }
        public Guid? RequestByTeamId { get; set; }
    }

    public class MatchRequestDTO
    {
        public Guid MatchRequestId { get; set; }
        public Guid MatchPostId { get; set; }
        public Guid RequestById { get; set; }
        public Guid? RequestByTeamId { get; set; }
        public MatchRequestStatus Status { get; set; } // IN_PROGRESS, ACCEPTED, REJECTED
        public DateTime RequestTime { get; set; }
        public DateTime? DecisionTime { get; set; }
    }
}

