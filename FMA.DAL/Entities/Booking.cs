using System;
using System.Collections.Generic;
using FMA.Common.Enums;

namespace FMA.DAL.Entities
{

    public class Booking
    {
        public int BookingId { get; set; }
        public int PitchId { get; set; }
        public int MatchPostId { get; set; }
        public int MatchRequestId { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime BookingTime { get; set; } // usually same as MatchPost.MatchTime
        public BookingStatus Status { get; set; } // Confirmed, Cancelled, etc.

        public virtual Pitch Pitch { get; set; } = null!;
        public virtual MatchPost MatchPost { get; set; } = null!;
        public virtual MatchRequest MatchRequest { get; set; } = null!;
    }
}

