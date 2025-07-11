using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMA.Common.DTOs
{
    public class TeamMemberDTO
    {
        public Guid TeamMemberId { get; set; }
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }
        public string? Position { get; set; }
        public DateTime JoinDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
