using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMA.Common.DTOs
{
    public class UserProfileDTO
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string? Position { get; set; }
        public DateTime? JoinDate { get; set; }

        public Guid? TeamId { get; set; }
        public string? TeamName { get; set; }
        public string? TeamDescription { get; set; }
    }

}
