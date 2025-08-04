using Response.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class CompanyService : i_CompanyService
{
    private readonly ApplicationDbContext _context;

    public CompanyService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DbSet<Company>> GetAllCompaniesAsync() =>
        await _context.Company;
            //.Include(c => c.Name);
            //.Include(c => c.CompanyLogo);
}