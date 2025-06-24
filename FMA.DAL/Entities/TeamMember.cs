using System;
using System.Collections.Generic;

namespace FMA.DAL.Entities;

public partial class TeamMember
{
    public int TeamId { get; set; }

    public int PlayerId { get; set; }

    public DateTime? JoinDate { get; set; }

    public virtual PlayerProfile Player { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
