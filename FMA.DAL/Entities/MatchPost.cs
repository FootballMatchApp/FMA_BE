using System;
using System.Collections.Generic;
using FMA.Common.Enums;

namespace FMA.DAL.Entities
{

    public class MatchPost
    {
        public Guid PostId { get; set; }
        public Guid PostById { get; set; }
        public Guid PitchId { get; set; }

        public Guid? PostByTeamId { get; set; }
        public virtual Team? PostByTeam { get; set; }

        public DateTime MatchTime { get; set; }
        public string? Description { get; set; }
        public PostStatus PostStatus { get; set; }

        public virtual User PostBy { get; set; } = null!;
        public virtual Pitch Pitch { get; set; } = null!;
        public virtual ICollection<MatchRequest> MatchRequests { get; set; } = new List<MatchRequest>();
    }
}

