using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.Common.Enums;

namespace FMA.DAL.Entities
{
    public class UserToken
    {
        public Guid TokenId { get; set; }
        public int UserId { get; set; } 
        public string TokenKey { get; set; } 
        public bool IsRevoked { get; set; }
        public TokenType Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
