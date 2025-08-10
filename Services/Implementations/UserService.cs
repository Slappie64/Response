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
        public async Task<IReadOnlyList<ApplicationUser>> GetAllAsync() =>
            await _db.Users.AsNoTracking().ToListAsync();

        // Get user by ID
        public Task<ApplicationUser?> GetUserByIdAsync(string userId) =>
            _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);

        // Get users by company ID
        public async Task<IReadOnlyList<ApplicationUser>> GetUserByCompanyAsync(int companyId) =>
            await _db.ApplicationUsers
                .Where(u => u.CompanyId == companyId)
                .AsNoTracking()
                .ToListAsync();
    }
}
