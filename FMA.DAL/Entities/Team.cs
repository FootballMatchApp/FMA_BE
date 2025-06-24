using System;
using System.Collections.Generic;

namespace FMA.DAL.Entities;

public partial class Team
{
    public int TeamId { get; set; }

    public string? TeamName { get; set; }

    public int CreatedBy { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Booking> BookingTeamBookings { get; set; } = new List<Booking>();

    public virtual ICollection<Booking> BookingTeamReceivings { get; set; } = new List<Booking>();

    public virtual PlayerProfile CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<MatchPost> MatchPostReceivingTeams { get; set; } = new List<MatchPost>();

    public virtual ICollection<MatchPost> MatchPostTeams { get; set; } = new List<MatchPost>();

    public virtual ICollection<MatchResult> MatchResultTeam1s { get; set; } = new List<MatchResult>();

    public virtual ICollection<MatchResult> MatchResultTeam2s { get; set; } = new List<MatchResult>();

    public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
}
