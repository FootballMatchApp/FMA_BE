using System;
using System.Collections.Generic;

namespace FMA.DAL.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Email { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    public virtual ICollection<PitchOwner> PitchOwners { get; set; } = new List<PitchOwner>();

    public virtual ICollection<PlayerProfile> PlayerProfiles { get; set; } = new List<PlayerProfile>();

    public virtual Role? Role { get; set; }
}
