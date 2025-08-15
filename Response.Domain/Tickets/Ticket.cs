using Response.Domain.Common;

namespace Response.Domain.Tickets;

public class Ticket : ITenantEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string HumanId { get; set; } = default!; // TCK-ACME-000001
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public string Status { get; set; } = "Open";
    public string Priority { get; set; } = "Medium"; // Low/Medium/High/Critical
    public Guid? AssignedUserId { get; set; }
    public DateTime CreatedAtUTC { get; set; } = DateTime.UtcNow;
    public DateTime? DueAtUTC { get; set; }
    public List<TicketComment> Comments { get; set; } = [];
}

public class TicketComment : ITenantEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid TicketId { get; set; }
    public string Body { get; set; } = default!;
    public DateTime CreatedAtUTC { get; set; } = DateTime.UtcNow;
}
