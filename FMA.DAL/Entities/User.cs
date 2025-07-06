using System;
using System.Collections.Generic;

namespace FMA.DAL.Entities
{

    public partial class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string PhoneNumber { get; set; } 
        public string Address { get; set; } 
        public string Email { get; set; }

        public Guid? RoleId { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<Pitch> Pitchs { get; set; } = new List<Pitch>();
        public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}
