namespace Response.Domain.Entities;

public class AppUser
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string EntraObjectId { get; set; } = default!;
    public string Role { get; set; } = default!;
    public ICollection<Ticket> AssignedTickets { get; set; } = [];
    public ICollection<Ticket> CreatedTickets { get; set; } = [];
}