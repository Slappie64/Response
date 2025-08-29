using Response.Domain.Enums;

namespace Response.Domain.Entities;

public class Ticket
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = default!;
    public string Reference { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public TicketStatus Status { get; set; } = TicketStatus.Open;
    public Guid? AssignedToId { get; set; }
    public AppUser? AssignedTo { get; set; }
    public Guid? CreatedById { get; set; } = default!;
    public AppUser CreatedBy { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Comment> Comments { get; set; } = [];

}