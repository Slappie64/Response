using Microsoft.EntityFrameworkCore;
using Response.Domain.Entities;
using Response.Infrastructure.Persistence.Extensions;

namespace Response.Infrastructure.Persistence;

public class AppDbContext : DbContext
{

    private readonly ITenantProvider? _tenant;
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public AppDbContext(DbContextOptions<AppDbContext> options, ITenantProvider tenant) : base(options)
    {
        _tenant = tenant;
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
        if (_tenant.HasValue)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.FindProperty("TenantId") != null)
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .HasQueryFilter(EntityTypeBuilderExtensions
                            .BuildLambdaForTenantFilter(entityType.ClrType, _tenant.Value));
                }
            }
        }

        // Tenants
        modelBuilder.Entity<Tenant>()
            .HasIndex(t => t.EntraTenantId)
            .IsUnique()
            .HasFilter("[EntraTenantId] IS NOT NULL");

        modelBuilder.Entity<AppUser>()
            .HasIndex(u => u.EntraObjectId)
            .IsUnique();

        // Tenant -> Users
        modelBuilder.Entity<AppUser>()
            .HasOne(u => u.Tenant)
            .WithMany(t => t.Users)
            .HasForeignKey(u => u.TenantId)
            .OnDelete(DeleteBehavior.Cascade);


        // Tenant -> Tickets
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Tenant)
            .WithMany(tn => tn.Tickets)
            .HasForeignKey(t => t.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        // Ticket.CreatedBy
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.CreatedBy)
            .WithMany()
            .HasForeignKey(t => t.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        // Ticket.AssignedTo
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.AssignedTo)
            .WithMany(u => u.AssignedTickets)
            .HasForeignKey(t => t.AssignedToId)
            .OnDelete(DeleteBehavior.NoAction);

        // Ticket -> Comments
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Ticket)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        // Comment -> User
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Author)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.NoAction);

        var tenantId = _tenant?.TenantId;
        if (tenantId != null)
        {
            modelBuilder.Entity<Tenant>().HasQueryFilter(e => e.Id == tenantId);
            modelBuilder.Entity<AppUser>().HasQueryFilter(e => e.TenantId == tenantId);
            modelBuilder.Entity<Ticket>().HasQueryFilter(e => e.TenantId == tenantId);
            modelBuilder.Entity<Comment>().HasQueryFilter(e => e.Ticket.TenantId == tenantId);
            modelBuilder.Entity<EmailTemplate>().HasQueryFilter(e => e.TenantId == tenantId);
            modelBuilder.Entity<TenantSequence>().HasQueryFilter(e => e.TenantId == tenantId);
        }

    }
}

