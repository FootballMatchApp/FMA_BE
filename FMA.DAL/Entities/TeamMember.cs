using System;
using System.Collections.Generic;

namespace FMA.DAL.Entities
{

    public partial class TeamMember
    {
        public int TeamId { get; set; }
        public int PlayerId { get; set; }
        public DateTime JoinDate { get; set; }
        public string? Position { get; set; }
        public virtual Team Team { get; set; } = null!;
    }
}
