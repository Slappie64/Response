using System;

namespace Response.Shared.Models;

public class Ticket
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Reference { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public TicketStatus Status { get; set; } = TicketStatus.New;
    public TicketPriority Priority { get; set; } = TicketPriority.Medium;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Inherited From Creator
    public Guid OrganisationId { get; set; }
    public Guid? DepartmentId { get; set; }

    // Relations
    public string CreatorId { get; set; } = default!;
    public ApplicationUser Creator { get; set; } = default!;
    public string? OwnerId { get; set; }
    public ApplicationUser? Owner { get; set; }
}