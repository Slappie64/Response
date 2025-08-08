using System.Collections.Generic;

namespace Response.Data
{
    public class Tag
    {
        // Primary key: uniquely identifies each tag record
        public int Id { get; set; } 

        // Optional name of the tag (e.g. "Bug", "UI", "High Priority")
        public string? Name { get; set; } 

        // Navigation property representing the many-to-many link between tags and tickets via the TicketTag join table
        public ICollection<TicketTag>? TicketTags { get; set; } 
    }
}