using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Response.Data;

namespace Response.Services
{
    public class SecurityGroupService : ISecurityGroupService
    {
        private readonly ApplicationDbContext _db;
        public SecurityGroupService(ApplicationDbContext db) => _db = db;

        // Get all security groups
        public Task<IReadOnlyList<SecurityGroup>> GetAllAsync() =>
            _db.SecurityGroups.AsNoTracking().ToListAsync().ContinueWith(t => (IReadOnlyList<SecurityGroup>)).t.Result;

        // Get security group by ID
        public Task<SecurityGroup?> GetSecurityGroupByIdAsync(int securityGroupId) =>
            _db.SecurityGroups.AsNoTracking().FirstOrDefaultAsync(sg => sg.SecurityId == securityGroupId);

        // Get security group by name
        public Task<SecurityGroup?> GetSecurityGroupByNameAsync(string securityGroupName) =>
            _db.SecurityGroups.AsNoTracking().FirstOrDefaultAsync(sg => sg.Name == securityGroupName);
    }
}