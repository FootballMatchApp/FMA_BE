using System;
using System.Collections.Generic;
using FMA.Common.Enums;

namespace FMA.DAL.Entities
{

    public partial class Pitch
    {
        public Guid PitchId { get; set; }

        public Guid OwnerId { get; set; }
        public User User { get; set; }

        public string? Name { get; set; }
        public string? ContactNumber { get; set; }

        public string? Location { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public decimal? PricePerHour { get; set; }

        public PitchStatus Status { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public virtual ICollection<MatchPost> MatchPosts { get; set; } = new List<MatchPost>();




    }
}
