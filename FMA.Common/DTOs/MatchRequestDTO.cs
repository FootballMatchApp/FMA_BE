using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.Common.Enums;

namespace FMA.Common.DTOs
{
    public class CreateMatchRequestDTO
    {
        public Guid MatchPostId { get; set; }
        public Guid? RequestByTeamId { get; set; }
        
    }
}

