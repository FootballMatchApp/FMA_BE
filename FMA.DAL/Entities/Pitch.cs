using System;
using System.Collections.Generic;

namespace FMA.DAL.Entities;

public partial class Pitch
{
    public int PitchId { get; set; }

    public int OwnerId { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public decimal? PricePerHour { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<MatchPost> MatchPosts { get; set; } = new List<MatchPost>();

    public virtual PitchOwner Owner { get; set; } = null!;
}
