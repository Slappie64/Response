using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Response.Data;

namespace Response.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext _db;
        public CompanyService(ApplicationDbContext db) => _db = db;

        // Get all companies
        public async Task<IReadOnlyList<Company>> GetAllAsync() =>
            await _db.Companies.AsNoTracking().ToListAsync();

        // Get company by ID
        public Task<Company?> GetCompanyByIdAsync(int companyId) =>
            _db.Companies.AsNoTracking().FirstOrDefaultAsync(c => c.CompanyId == companyId);

        // Get company by name
        public Task<Company?> GetCompanyByNameAsync(string companyName) =>
            _db.Companies.AsNoTracking().FirstOrDefaultAsync(c => c.Name == companyName);
    }
}