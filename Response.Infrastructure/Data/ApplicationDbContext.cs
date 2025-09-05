using Response.Domain.Entities;

namespace Response.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
        
    }
}