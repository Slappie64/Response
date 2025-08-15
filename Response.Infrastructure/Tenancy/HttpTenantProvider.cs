using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Response.Infrastructure.Persistence;

namespace Response.Infrastructure.Tenancy;

public class HttpTenantProvider : ITenantProvider
{
    private readonly IHttpContextAccessor _http;
    private readonly AppDbContext _db;
    private Guid? _tenantId;
    private string? _tenantCode;

    public HttpTenantProvider(IHttpContextAccessor http, AppDbContext db)
    {
        _http = http;
        _db = db;
    }

    public Guid? CurrentTenantId => _tenantId;
    public string? TenantCode => _tenantCode;

    public void SetTenant(Guid tenantId, string tenantCode)
    {
        _tenantId = tenantId;
        _tenantCode = tenantCode;
    }

    public async Task EnsureResolvedAsync()
    {
        if (_tenantId.HasValue) return;

        var user = _http.HttpContext?.User;
        var tid = user?.FindFirstValue("tid"); //Entra Tenant ID (GUID)
        if (tid is null) return;

        var tenant = await _db.Tenants.AsNoTracking()
            .SingleOrDefaultAsync(t => t.EntraTenantId == tid);

        if (tenant is not null)
            SetTenant(tenant.Id, tenant.Code);
        // Else trigger onboarding flow (Deferred for MVP)
    }
}