using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Response.Domain.Entities;
using Response.Infrastructure.Persistence;

namespace Response.Infrastructure.Tenancy;

public class UserProvisioner : IUserProvisioner
{
    private readonly AppDbContext _db;
    private readonly ITenantProvider _tenant;

    public UserProvisioner(AppDbContext db, ITenantProvider tenant)
    {
        _db = db;
        _tenant = tenant;
    }

    public async Task ProvisionAsync(ClaimsPrincipal principal)
    {
        if (!principal.Identity?.IsAuthenticated ?? true) return;
        await _tenant.EnsureTenantAsync(principal);

        var tenantId = _tenant.TenantId ?? throw new InvalidOperationException("Tenant Not Resolved");
        var oid = principal.FindFirstValue("oid") // Entra ID Object ID claim
            ?? principal.FindFirstValue(ClaimTypes.NameIdentitfier)
            ?? throw new InvalidOperationException("User Object ID Claim Not Found");

        var email = principal.FindFirstValue("preferred_username")
            ?? principal.FindFirstValue(ClaimTypes.Email)
            ?? "unknown@local";

        var name = principal.FindFirstValue("name")
            ?? principal.Identity?.Name
            ?? email;

        var user = await _db.Users.IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.EntraObjectId == oid && u.TenantId == tenantId);

        if (user == null)
        {
            user = new AppUser
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                DisplayName = name,
                Email = email,
                EntraObjectId = oid,
                Role = "Viewer" // Default Role
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }
    }
}