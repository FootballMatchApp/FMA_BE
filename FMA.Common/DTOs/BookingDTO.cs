using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMA.Common.DTOs
{
    public class BookingDTO
    {
        public Guid BookingId { get; set; }
        public Guid PitchId { get; set; }
        public Guid MatchPostId { get; set; }
        public Guid MatchRequestId { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime BookingTime { get; set; }
        public string Status { get; set; } = null!;
    }
    public class CreateBookingDTO
    {
        public Guid PitchId { get; set; }
        public Guid MatchPostId { get; set; }
        public Guid MatchRequestId { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime BookingTime { get; set; }
    }
    public class UpdateBookingDTO
    {
        public Guid BookingId { get; set; }
        public Guid PitchId { get; set; }
        public Guid MatchPostId { get; set; }
        public Guid MatchRequestId { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime BookingTime { get; set; }
        public string Status { get; set; } = null!;
    }
}
