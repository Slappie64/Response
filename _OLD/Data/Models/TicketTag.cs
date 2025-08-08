namespace Response.Data
{
    public class TicketTag
    {
        // Foreign key: identifies the ticket this tag is linked to
        public int? TicketId { get; set; }

        // Navigation property to the related Ticket entity
        public Ticket? Ticket { get; set; }

        // Foreign key: identifies the tag applied to the ticket
        public int? TagId { get; set; }

        // Navigation property to the related Tag entity
        public Tag? Tag { get; set; }
    }
}