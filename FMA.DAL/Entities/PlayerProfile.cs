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
    }
} 