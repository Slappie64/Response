using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Response.Data;

namespace Response.Services
{
    public class UserService : IUserService
    {

        private readonly ApplicationDbContext _db;
        public UserService(ApplicationDbContext db) => _db = db;

        // Get all users
        public Task<IReadOnlyList<ApplicationUser>> GetAllAsync() =>
            _db.ApplicationUsers.AsNoTracking().ToListAsync().ContinueWith(t => (IReadOnlyList < ApplicationUser)).t.Result;

        // Get user by ID
        public Task<ApplicationUser?> GetUserByIdAsync(int userId) =>
            _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);

        // Get users by company ID
        public Task<IReadOnlyList<ApplicationUser>> GetUserByCompanyAsync(int companyId) =>
            _db.ApplicationUsers
                .Where(u => u.CompanyId == companyId)
                .AsNoTracking()
                .ToListAsync()
                .ContinueWith(t => (IReadOnlyList<ApplicationUser>)).t.Result;

        // Get users by department ID
        public Task<IReadOnlyList<ApplicationUser>> GetUserByDepartmentAsync(int departmentId) =>
            _db.ApplicationUsers
                .Where(u => u.DepartmentId == departmentId)
                .AsNoTracking()
                .ToListAsync()
                .ContinueWith(t => (IReadOnlyList<ApplicationUser>)).t.Result;
        
        // Get users by security group ID
        public Task<IReadOnlyList<ApplicationUser>> GetUserBySecurityGroupAsync(int securityGroupId ) =>
            _db.ApplicationUsers
                .Where(u => u.SecurityGroupId == securityGroupId)
                .AsNoTracking()
                .ToListAsync()
                .ContinueWith(t => (IReadOnlyList<ApplicationUser>)).t.Result;
    }

}
