using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Response.Data;

namespace Response.Data {
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Group> Groups { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<GroupPermission> GroupPermissions { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketTag> TicketTags { get; set; }
    public DbSet<TicketComment> TicketComments { get; set; }
    public DbSet<TicketAttachment> TicketAttachments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) {
      base.OnModelCreating(builder);

      // Composite keys
      builder.Entity<UserGroup>().HasKey(ug => new { ug.UserId, ug.GroupId });
      builder.Entity<TicketTag>().HasKey(tt => new { tt.TicketId, tt.TagId });

      // Relationships
      builder.Entity<UserGroup>()
        .HasOne(ug => ug.User).WithMany(u => u.UserGroups).HasForeignKey(ug => ug.UserId);
      builder.Entity<UserGroup>()
        .HasOne(ug => ug.Group).WithMany(g => g.UserGroups).HasForeignKey(ug => ug.GroupId);

      builder.Entity<GroupPermission>()
        .HasOne(p => p.Group).WithMany(g => g.Permissions).HasForeignKey(p => p.GroupId);

      // Add any default seed data here if desired
    }
  }
}