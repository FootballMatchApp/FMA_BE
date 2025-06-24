using System;
using System.Collections.Generic;

namespace FMA.DAL.Entities;

public partial class MatchResult
{
    public int ResultId { get; set; }

    public int Team1Id { get; set; }

    public int Team2Id { get; set; }

    public DateTime MatchDate { get; set; }

    public int? ScoreTeam1 { get; set; }

    public int? ScoreTeam2 { get; set; }

    public virtual Team Team1 { get; set; } = null!;

    public virtual Team Team2 { get; set; } = null!;
}
