using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Response.Data;

namespace Response.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _db;
        public DepartmentService(ApplicationDbContext db) => _db = db;

        // Get all Departments
        public async Task<IReadOnlyList<Department>> GetAllAsync() =>
            await _db.Departments.AsNoTracking().ToListAsync();

        // Get Department by ID
        public Task<Department?> GetDepartmentByIdAsync(int departmentId) =>
            _db.Departments.AsNoTracking().FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
        
        // Get Department by Name
        public Task<Department?> GetDepartmentByNameAsync(string departmentName) =>
            _db.Departments.AsNoTracking().FirstOrDefaultAsync(d => d.Name == departmentName);

    }
}