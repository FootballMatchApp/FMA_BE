using System;

namespace FMA.Common.DTOs
{
    public class CreateTeamDTO
    {
        public string TeamName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class UpdateTeamDTO
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
} 