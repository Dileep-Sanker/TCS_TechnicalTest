using System;

namespace TCSTest.Models
{
    public  class Channel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Provider { get; set; }
        public string? Language { get; set; }
        public string? Region { get; set; }
    }
}
// This code defines a Channel class with properties for Id, Name, and Description.