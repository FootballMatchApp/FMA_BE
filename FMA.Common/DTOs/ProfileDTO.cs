using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMA.Common.DTOs
{
    public class ProfileDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TeamId { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; } // FE sẽ truyền hoặc cập nhật
        public string Phone { get; set; }
        public int? Age { get; set; }
        public string Position { get; set; }
        public string Bio { get; set; }
    }
}
