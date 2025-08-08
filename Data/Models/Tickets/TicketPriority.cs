namespace Response.Data;

public class TicketPriority
{
    public int PriorityId { get; set; }
    public string Name { get; set; } // e.g. "Low", "Medium", "High", "Critical"
    public string? Description { get; set; }

    public ICollection<Ticket> Tickets { get; set; }
}