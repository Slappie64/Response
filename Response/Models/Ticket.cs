namespace Response.Models;

public enum TicketStatus { New, InProgress, Resolved, Closed }
public enum TicketPriority { P4, P3, P2, P1 }

public class Ticket
{
    public Guid Id { get; set; }
    public string TicketId { get; set; } = default!;

    public string Title { get; set; } = default!;
    public string? Description { get; set; }

    public TicketStatus Status { get; set; } = TicketStatus.New;
    public TicketPriority Priority { get; set; } = TicketPriority.P3;

    public DateTime CreateAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string CreatedById { get; set; } = default!;
    public ApplicationUser CreatedBy { get; set; } = default!;

    public string? UpdatedById { get; set; }
    public ApplicationUser UpdatedBy { get; set; }

    public string? OwnerId { get; set; }
    public ApplicationUser Owner { get; set; }

    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = default!;

}