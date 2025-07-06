using System;
using System.Collections.Generic;
using FMA.Common.Enums;

namespace FMA.DAL.Entities
{

    public class Booking
    {
        public Guid BookingId { get; set; }
        public Guid PitchId { get; set; }
        public Guid MatchPostId { get; set; }
        public Guid MatchRequestId { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime BookingTime { get; set; } // usually same as MatchPost.MatchTime
        public BookingStatus Status { get; set; } // Confirmed, Cancelled, etc.

        public virtual Pitch Pitch { get; set; } = null!;
        public virtual MatchPost MatchPost { get; set; } = null!;
        public virtual MatchRequest MatchRequest { get; set; } = null!;
    }
}

