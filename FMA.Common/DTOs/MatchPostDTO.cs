using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.Common.Enums;

namespace FMA.Common.DTOs
{
    public class CreateMatchPostDTO
    {
        public Guid PitchId { get; set; }
        public DateTime MatchTime { get; set; }
        public string? Description { get; set; }
        public Guid? PostByTeamId { get; set; } // Nullable to allow for individual posts
    }
}

