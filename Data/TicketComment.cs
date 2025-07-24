using System;

namespace Response.Data
{
    public class TicketComment
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}