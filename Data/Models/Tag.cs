using System.Collections.Generic;

namespace Response.Data
{
    public class Tag
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<TicketTag>? TicketTags { get; set; }
    }
}