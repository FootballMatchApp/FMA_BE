﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FMA.DAL.Entities
{

    public partial class Role
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
