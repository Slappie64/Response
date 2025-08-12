using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Response.Services;

public interface ICurrentUser
{
    Task<string?> GetUserIdAsync();
    Task<bool> IsInRoleAsync(string role);
    Task<Guid?> GetCompanyIdAsync();
    Task<Guid?> GetDepartmentIdAsync();
}

public class CurrentUser : ICurrentUser
{
    private readonly AuthenticationStateProvider _auth;
    private readonly Data.ApplicationDbContext _db;

    public CurrentUser(AuthenticationStateProvider auth, Data.ApplicationDbContext db)
    {
        _auth = auth;
        _db = db;
    }

    public async Task<string?> GetUserIdAsync()
    {
        var state = await _auth.GetAuthenticationStateAsync();
        return state.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public async Task<bool> IsInRoleAsync(string role)
    {
        var state = await _auth.GetAuthenticationStateAsync();
        return state.User.IsInRole(role);
    }

    public async Task<Guid?> GetCompanyIdAsync()
    {
        var userId = await GetUserIdAsync();
        if (userId is null) return null;
        var user = await _db.Users.FindAsync(userId);
        return user?.CompanyId;
    }

    public async Task<Guid?> GetDepartmentIdAsync()
    {
        var userId = await GetUserIdAsync();
        if (userId is null) return null;
        var user = await _db.Users.FindAsync(userId);
        return user?.DepartmentId;
    }
}