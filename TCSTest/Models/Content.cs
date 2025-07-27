using System;

namespace TCSTest.Models
{
    public class Content
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // Movie or Show
        public string? Genre { get; set; }
        public string? Description { get; set; }
        public int? ReleaseYear { get; set; }
        public int? Duration { get; set; } // Duration in minutes
        public string? Rating { get; set; } // e.g., "PG-13", "TV-MA"
    }
}
