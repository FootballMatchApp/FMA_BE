using System;
using System.Collections.Generic;

namespace FMA.DAL.Entities;

public partial class MatchPost
{
    public int PostId { get; set; }

    public int TeamId { get; set; }

    public int PostedByPlayerId { get; set; }

    public int PitchId { get; set; }

    public int? ReceivingTeamId { get; set; }

    public DateTime MatchTime { get; set; }

    public string? Description { get; set; }

    public string? LookingFor { get; set; }

    public string? PostStatus { get; set; }

    public virtual Pitch Pitch { get; set; } = null!;

    public virtual PlayerProfile PostedByPlayer { get; set; } = null!;

    public virtual Team? ReceivingTeam { get; set; }

    public virtual Team Team { get; set; } = null!;
}
