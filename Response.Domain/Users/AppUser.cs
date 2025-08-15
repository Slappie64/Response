using Response.Domain.Common;

namespace Response.Domain.Users;

public class AppUser : ITenantEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string DisplayName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Role { get; set; } = "Agent";
    public DateTime CreatedAtUTC { get; set; } = DateTime.UtcNow;
}