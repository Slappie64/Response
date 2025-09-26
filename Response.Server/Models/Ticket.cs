using System.ComponentModel.DataAnnotations;

namespace Response.Server.Models;

public class Ticket
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    public TicketStatus Status { get; set; } = TicketStatus.Open;
    public TicketPriority Priority { get; set; } = TicketPriority.Medium;

    [Required]
    public string CreatedById { get; set; } = string.Empty;
    public string? AssignedToId { get; set; }

    [Required]
    public int TenantId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public virtual ApplicationUser CreatedBy { get; set; } = null!;
    public virtual ApplicationUser? AssignedTo { get; set; }
    public virtual Tenant Tenant { get; set; } = null!;
}

public enum TicketStatus
{
    Open = 0,
    Inprogres = 1,
    Closed = 2
}

public enum TicketPriority
{
    Low = 0,
    Medium = 1,
    High = 2,
    Critical = 3
}