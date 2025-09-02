using System.Security.Claims;
using Response.Domain.Entities;

namespace Response.Infrastructure.Tenancy;

public interface ITenantProvider
{
    Guid? TenantId { get; }
    Tenant? Tenant { get; }
    Task EnsureTenantAsync(ClaimsPrincipal principal);
}