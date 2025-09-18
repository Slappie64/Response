using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Response.Shared.Models;

namespace Response.Server.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Organisation> Organisations => Set<Organisation>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Ticket> Tickets => Set<Ticket>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        // Organisation
        b.Entity<Organisation>()
            .HasMany(o => o.Departments)
            .WithOne(d => d.Organisation)
            .HasForeignKey(d => d.OrganisationId)
            .OnDelete(DeleteBehavior.Cascade);

        // ApplicationUser
        b.Entity<ApplicationUser>()
            .HasOne(u => u.Organisation)
            .WithMany()
            .HasForeignKey(u => u.OrganisationId)
            .OnDelete(DeleteBehavior.Restrict);

        b.Entity<ApplicationUser>()
            .HasOne(u => u.Department)
            .WithMany()
            .HasForeignKey(u => u.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Ticket
        b.Entity<Ticket>()
            .HasOne(t => t.Creator)
            .WithMany()
            .HasForeignKey(t => t.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);

        b.Entity<Ticket>()
            .HasOne(t => t.Owner)
            .WithMany()
            .HasForeignKey(t => t.OwnerId)
            .OnDelete(DeleteBehavior.SetNull);

        b.Entity<Ticket>()
            .Property(t => t.Reference)
            .IsRequired()
            .HasMaxLength(32);

        b.Entity<Ticket>()
            .HasIndex(t => t.Reference)
            .IsUnique();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<Ticket>();
        var now = DateTime.UtcNow;
        foreach (var e in entries)
        {
            if (e.State == EntityState.Added) e.Entity.CreatedAt = now;
            e.Entity.UpdatedAt = now;
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}