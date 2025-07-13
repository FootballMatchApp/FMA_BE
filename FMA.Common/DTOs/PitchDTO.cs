using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMA.Common.DTOs
{
    public class CreatePitchDTO
    {
        public string? Name { get; set; }
        public string? ContactNumber { get; set; }

        public string? Location { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public decimal? PricePerHour { get; set; }
    }

    public class UpdatePitchDTO
    {
        public Guid PitchId { get; set; }
        public string? Name { get; set; }
        public string? ContactNumber { get; set; }

        public string? Location { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public decimal? PricePerHour { get; set; }
    }

    public class GetPitchDTO
    {
        public Guid PitchId { get; set; }
        public string? Name { get; set; }
        public string? ContactNumber { get; set; }
        public string? Location { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public decimal? PricePerHour { get; set; }
        public FMA.Common.Enums.PitchStatus Status { get; set; }
    }
}
