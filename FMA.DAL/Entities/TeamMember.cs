using System;
using System.Collections.Generic;

namespace FMA.DAL.Entities
{

    public partial class TeamMember
    {
        public Guid TeamId { get; set; }
        public Guid TeamMemberId { get; set; }
        public DateTime JoinDate { get; set; }
        public string? Position { get; set; }
        public virtual Team Team { get; set; } = null!;
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
