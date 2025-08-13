using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Response.Data;
using Response.Models;

namespace Response.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Sequence> Sequences => Set<Sequence>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        b.Entity<Company>(e =>
        {
            e.HasIndex(x => x.Name).IsUnique();
        });

        b.Entity<Department>(e =>
        {
            e.HasOne(x => x.Company)
                .WithMany(x => x.Departments)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasIndex(x => new { x.CompanyId, x.Name }).IsUnique();
        });

        b.Entity<ApplicationUser>(e =>
        {
            e.HasIndex(x => x.UserId).IsUnique();
            e.HasOne(x => x.Company)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Department)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        b.Entity<Ticket>(e =>
        {
            e.HasIndex(x => x.TicketId).IsUnique();

            e.HasOne(x => x.CreatedBy)
             .WithMany(x => x.CreatedTickets)
             .HasForeignKey(x => x.CreatedById)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.UpdatedBy)
             .WithMany()
             .HasForeignKey(x => x.UpdatedById)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Owner)
             .WithMany(x => x.OwnedTickets)
             .HasForeignKey(x => x.OwnerId)
             .OnDelete(DeleteBehavior.SetNull);

            e.HasOne(x => x.Company)
             .WithMany()
             .HasForeignKey(x => x.CompanyId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        b.Entity<Sequence>(e =>
        {
            e.HasIndex(x => new { x.Scope, x.Year }).IsUnique();
            e.Property(x => x.RowVersion).IsRowVersion();
        });
    }
}
