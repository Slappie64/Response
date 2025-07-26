namespace Response.Data
{
    public class TicketTag
    {
        public int? TicketId { get; set; }
        public Ticket? Ticket { get; set; }

        public int? TagId { get; set; }
        public Tag? Tag { get; set; }
    }
}