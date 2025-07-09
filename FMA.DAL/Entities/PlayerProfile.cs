using System;
using System.ComponentModel.DataAnnotations;

namespace FMA.DAL.Entities
{
    public class PlayerProfile
    {
        [Key]
        public int PlayerId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public string Bio { get; set; } = string.Empty;
        public int? Age { get; set; }
        public string Avatar { get; set; } = string.Empty;
    }
} 