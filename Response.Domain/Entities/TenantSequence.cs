namespace Response.Domain.Entities;

public class TenantSequence
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = default!;
    public string SequenceName { get; set; } = default!;
    public int NextValue { get; set; }
}