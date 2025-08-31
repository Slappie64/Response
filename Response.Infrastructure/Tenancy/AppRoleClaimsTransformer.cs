using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Response.Infrastructure.Persistence;

namespace Response.Infrastructure.Tenancy;

public class AppRoleClaimsTransformer : IClaimsTransformation
{
    private readonly AppDbContext _db;

    public AppRoleClaimsTransformer(AppDbContext db) => _db = db;

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (!principal.Identity?.IsAuthenticated ?? true) return principal;

        var oid = principal.FindFirstValue("oid");
        if (string.IsNullOrEmpty(oid)) return principal;

        var user = await _db.Users.FirstOrDefaultAsync(u => u.EntraObjectId == oid);
        if (user == null) return principal;

        var identity = (ClaimsIdentity)principal.Identity;
        // Avoid Duplicate Role Claims
        if (!identity.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == user.Role))
            identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
    

        return principal;
    }
}