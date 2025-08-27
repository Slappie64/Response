using Microsoft.EntityFrameworkCore;

namespace Response.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    // DBSets will be added at a later time
}