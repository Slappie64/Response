using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Response.Data;

namespace Response.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly ApplicationDbContext _db;
        public PermissionService(ApplicationDbContext db) => _db = db;

        // Get all permissions
        public async Task<IReadOnlyList<Permission>> GetAllAsync() =>
            await _db.Permissions.AsNoTracking().ToListAsync();

        // Get permission by ID
        public Task<Permission?> GetPermissionByIdAsync(int permissionId) =>
            _db.Permissions.AsNoTracking().FirstOrDefaultAsync(p => p.PermissionId == permissionId);

        // Get permission by name
        public Task<Permission?> GetPermissionByNameAsync(string permissionName) =>
            _db.Permissions.AsNoTracking().FirstOrDefaultAsync(p => p.Name == permissionName);
    }
}