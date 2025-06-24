using System;
using System.Collections.Generic;

namespace FMA.DAL.Entities;

public partial class PlayerProfile
{
    public int PlayerId { get; set; }

    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Position { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<MatchPost> MatchPosts { get; set; } = new List<MatchPost>();

    public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    public virtual User User { get; set; } = null!;
}
