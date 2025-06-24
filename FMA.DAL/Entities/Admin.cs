using System;
using System.Collections.Generic;

namespace FMA.DAL.Entities;

public partial class Admin
{
    public int AdminId { get; set; }

    public int UserId { get; set; }

    public string? FullName { get; set; }

    public virtual User User { get; set; } = null!;
}
