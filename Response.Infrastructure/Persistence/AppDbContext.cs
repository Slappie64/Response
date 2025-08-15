using Microsoft.EntityFrameworkCore;

using Response.Domain.Tenancy;
using Response.Domain.Users;
using Response.Domain.Tickets;
using Response.Domain.Common;
using Response.Infrastructure.Tenancy;
using System.Security.Cryptography.X509Certificates;

namespace Response.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private readonly ITenantProvider _tenant;

    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<TicketComment> TicketComments => Set<TicketComment>();
    public DbSet<TenantSequence> TenantSequences => Set<TenantSequence>();
    public DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();

    public AppDbContext(DbContextOptions<AppDbContext> options, ITenantProvider tenant) : base(options)
    {
        _tenant = tenant;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Global Tenant Filter
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITenantEntity).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(AppDbContext).GetMethod(nameof(SetTenantFilter),
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
                    .MakeGenericMethod(entityType.ClrType);
                method.Invoke(null, new object[] { modelBuilder, this });
            }
        }

        modelBuilder.Entity<Tenant>(b =>
        {
            b.HasIndex(t => t.EntraTenantId).IsUnique();
            b.HasIndex(t => t.Code).IsUnique();
        });

        modelBuilder.Entity<Ticket>(b =>
        {
            b.HasIndex(t => new { t.TenantId, t.HumanId }).IsUnique();
            b.Property(t => t.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<AppUser>(b =>
        {
            b.HasIndex(u => new { u.TenantId, u.Email }).IsUnique();
        });

        modelBuilder.Entity<TenantSequence>(b =>
        {
            b.HasKey(x => new { x.TenantId, x.Entity });
        });

        modelBuilder.Entity<EmailTemplate>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.TenantId, x.TemplateType }).IsUnique();
        });
    }

    private static void SetTenantFilter<TEntity>(ModelBuilder modelBuilder, AppDbContext ctx)
        where TEntity : class, ITenantEntity
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(e =>
            ctx._tenant.CurrentTenantId != null && e.TenantId == ctx._tenant.CurrentTenantId.Value);
    }
}