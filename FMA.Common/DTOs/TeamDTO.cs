using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMA.Common.DTOs
{

    public class CreateTeamDTO
    {
        public string TeamName { get; set; }
        public string? Description { get; set; }

    }

    public class UpdateTeamDTO 
    {
        public Guid TeamId { get; set; }
        public string TeamName { get; set; }
        public string? Description { get; set; }

    }

    public class GetTeamDTO
    {
        public Guid TeamId { get; set; }
        public string? TeamName { get; set; }
        public string? Description { get; set; }

         
        
    }


    }
