using System;
using System.Collections.Generic;

namespace FMA.DAL.Entities { 

    public partial class Team
    {
        public Guid TeamId { get; set; }
        public string? TeamName { get; set; }
        public Guid CreatedById { get; set; }
        public User CreateBy { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
    }
}
