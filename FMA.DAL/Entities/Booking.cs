using System;
using System.Collections.Generic;

namespace FMA.DAL.Entities;

public partial class Booking
{
    public int BookingId { get; set; }

    public int PitchId { get; set; }

    public int? PlayerId { get; set; }

    public int? TeamBookingId { get; set; }

    public int? TeamReceivingId { get; set; }

    public DateTime BookingTime { get; set; }

    public int? DurationHours { get; set; }

    public string? Status { get; set; }

    public virtual Pitch Pitch { get; set; } = null!;

    public virtual PlayerProfile? Player { get; set; }

    public virtual Team? TeamBooking { get; set; }

    public virtual Team? TeamReceiving { get; set; }
}
