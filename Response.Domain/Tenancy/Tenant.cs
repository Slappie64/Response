namespace Response.Domain.Tenancy;

public class Tenant
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string EntraTenantId { get; set; } = default!;
    public DateTime CreatedAtUTC { get; set; } = DateTime.UtcNow;
}