using Microsoft.EntityCoreFramework;
using Response.Infrastructure.Persistence;

namespace Response.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    // Add DB Sets in a later stage
}