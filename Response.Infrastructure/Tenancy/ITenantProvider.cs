namespace Response.Infrastructure.Tenancy;

public interface ITenantProvider
{
    Guid? CurrentTenantId { get; }
    string? TenantCode { get; }
    void SetTenant(Guid tenantId, string tenantCode);
}