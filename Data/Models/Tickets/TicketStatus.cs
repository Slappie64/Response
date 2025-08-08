namespace Response.Data;


public class TicketStatus
{
    public int StatusId { get; set; }
    public string Name { get; set; } // e.g. "Open", "In Progress", "Resolved", "Closed"
    public string? Description { get; set; }

    public ICollection<Ticket> Tickets { get; set; }
}