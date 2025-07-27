using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Response.Data;

namespace Response.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Table for groups of users
        public DbSet<Group> Groups { get; set; }

        // Join table linking users to groups
        public DbSet<UserGroup> UserGroups { get; set; }

        // Permissions assigned to groups
        public DbSet<GroupPermission> GroupPermissions { get; set; }

        // Departments associated with tickets
        public DbSet<Department> Departments { get; set; }

        // Status options for tickets
        public DbSet<Status> Status { get; set; }

        // Tags used to categorize tickets
        public DbSet<Tag> Tags { get; set; }

        // Core entity representing support tickets
        public DbSet<Ticket> Tickets { get; set; }

        // Join table linking tickets to tags
        public DbSet<TicketTag> TicketTags { get; set; }

        // Comments left on tickets
        public DbSet<TicketComment> TicketComments { get; set; }

        // File attachments linked to tickets
        public DbSet<TicketAttachment> TicketAttachments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Composite key for UserGroup join table
            builder.Entity<UserGroup>().HasKey(ug => new { ug.UserId, ug.GroupId });

            // Composite key for TicketTag join table
            builder.Entity<TicketTag>().HasKey(tt => new { tt.TicketId, tt.TagId });

            // UserGroup relationships
            builder.Entity<UserGroup>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId);

            builder.Entity<UserGroup>()
                .HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId);

            // GroupPermission relationship
            builder.Entity<GroupPermission>()
                .HasOne(p => p.Group)
                .WithMany(g => g.Permissions)
                .HasForeignKey(p => p.GroupId);
        }
    }
}