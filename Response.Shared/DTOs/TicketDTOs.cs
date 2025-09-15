using System;
using Response.Shared.Models;

namespace Response.Shared.DTOs;

public class TicketCreateRequest
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public TicketPriority Priority { get; set; } = TicketPriority.Medium;
    public string? OwnerId { get; set; }
}

public class TicketResponse
{
    public Guid Id { get; set; }
    public string Reference { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public TicketStatus Status { get; set; }
    public TicketPriority Priority { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string CreatorEmail { get; set; } = default!;
    public string OwnerEmail { get; set; } = default!;
}