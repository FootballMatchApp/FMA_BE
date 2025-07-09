using System;
using System.Collections.Generic;

namespace FMA.DAL.Entities { 

    public partial class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int CreatedBy { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
    }
}
