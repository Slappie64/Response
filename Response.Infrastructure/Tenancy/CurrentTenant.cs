using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Response.Domain.Entities;
using Response.Infrastructure.Persistence;

namespace Response.Infrastructure.Tenancy;

public class CurrentTenant : ITenantProvider
{
    private const string TenantItemKey = "__tenant__";

    private readonly IHttpContextAccessor _http;
    private readonly DbContextOptions<AppDbContext> _options;

    public CurrentTenant(IHttpContextAccessor http, DbContextOptions<AppDbContext> options)
    {
        _http = http;
        _options = options;
    }

    public Guid? TenantId => Tenant?.Id;
    public Tenant? Tenant
    {
        get => _http.HttpContext?.Items[TenantItemKey] as Tenant;
        private set
        {
            if (_http.HttpContext != null)
            {
                _http.HttpContext.Items[TenantItemKey] = value;
            }
        }
    }

    public async Task EnsureTenantAsync(ClaimsPrincipal principal)
    {
        if (_http.HttpContext == null) return;
        if (Tenant != null) return;
        if (!principal.Identity?.IsAuthenticated ?? true) return;

        var tid = principal.FindFirstValue("tid"); // Entra ID Tenant ID claim
        var upn = principal.FindFirstValue("preferred_username")
            ?? principal.FindFirstValue(ClaimTypes.Email)
            ?? principal.FindFirstValue("upn");

        using var db = new AppDbContext(_options);
        Tenant? tenant = null;

        if (Guid.TryParse(tid, out var entraTid))
            tenant = await db.Tenants.FirstOrDefaultAsync(t => t.EntraTenantId == entraTid);
        if (tenant == null && !string.IsNullOrWhiteSpace(upn) && upn.Contains('@'))
        {
            string domain = upn.Split('@')[1].ToLowerInvariant();
            tenant = await db.Tenants.FirstOrDefaultAsync(t => t.Domain == domain);
        }

        // Automatic tenant provisioning
        if (tenant == null)
        {
            tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = upn ?? "Unknown",
                Domain = upn?.Split('@')[1].ToLowerInvariant() ?? "unknown.local",
                EntraTenantId = Guid.TryParse(tid, out var et) ? et : null
            };
            db.Tenants.Add(tenant);
            await db.SaveChangesAsync();
        }

        Tenant = tenant;
    }
}