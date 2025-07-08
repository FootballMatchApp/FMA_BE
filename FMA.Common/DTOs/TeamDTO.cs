using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMA.Common.DTOs
{
    public class TeamListDTO
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public int CreatedByUserId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty; // FE sẽ truyền hoặc cập nhật
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
