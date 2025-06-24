using System;
using System.Collections.Generic;

namespace FMA.DAL.Entities;

public partial class PitchOwner
{
    public int OwnerId { get; set; }

    public int UserId { get; set; }

    public string? OwnerName { get; set; }

    public string? ContactNumber { get; set; }

    public virtual ICollection<Pitch> Pitches { get; set; } = new List<Pitch>();

    public virtual User User { get; set; } = null!;
}
