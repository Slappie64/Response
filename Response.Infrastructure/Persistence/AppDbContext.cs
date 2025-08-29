using Microsoft.EntityFrameworkCore;
using Response.Domain.Entities;
using Response.Infrastructure.Persistence.Extensions;

namespace Response.Infrastructure.Persistence;

public class AppDbContext : DbContext
{

    private readonly Guid? _currentTenantId;
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public AppDbContext(DbContextOptions<AppDbContext> options, Guid currentTenantId) : base(options)
    {
        _currentTenantId = currentTenantId;
    }

    // Add DbSets for your entities here
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();
    public DbSet<TenantSequence> TenantSequences => Set<TenantSequence>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Tenant Filter
        if (_currentTenantId.HasValue)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.FindProperty("TenantId") != null)
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .HasQueryFilter(EntityTypeBuilderExtensions
                            .BuildLambdaForTenantFilter(entityType.ClrType, _currentTenantId.Value));
                }
            }
        }

        // Tenant -> Users
        modelBuilder.Entity<AppUser>()
            .HasOne<Tenant>()
            .WithMany(t => t.Users)
            .HasForeignKey(u => u.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        // Tenant -> Tickets
        
    }
}

