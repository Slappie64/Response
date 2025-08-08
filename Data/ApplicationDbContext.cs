using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Response.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<SecurityGroup> SecurityGroups { get; set; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketComment> TicketComments { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
        public DbSet<TicketPriority> TicketPriorities { get; set; }
        public DbSet<TicketAttachment> TicketAttachments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed TicketPriority
            modelBuilder.Entity<TicketPriority>().HasData(
                new TicketPriority { PriorityId = 4, Name = "P4", Description = "Minor issue, no urgency" },
                new TicketPriority { PriorityId = 3, Name = "P3", Description = "Standard priority" },
                new TicketPriority { PriorityId = 2, Name = "P2", Description = "Needs prompt attention" },
                new TicketPriority { PriorityId = 1, Name = "P1", Description = "Critical, business impact" }
            );

            // Seed TicketStatus
            modelBuilder.Entity<TicketStatus>().HasData(
                new TicketStatus { StatusId = 1, Name = "Open", Description = "Ticket has been created" },
                new TicketStatus { StatusId = 2, Name = "In Progress", Description = "Work is underway" },
                new TicketStatus { StatusId = 3, Name = "Waitng", Description = "Ticket is waiting on external" },
                new TicketStatus { StatusId = 4, Name = "Resolved", Description = "Issue has been addressed" },
                new TicketStatus { StatusId = 5, Name = "Closed", Description = "Ticket is finalized" }
            );

            // SecurityGroup: Primary Key
            builder.Entity<SecurityGroup>()
                .HasKey(g => g.SecurityId);

            // ApplicationUserGroup: Composite Key
            builder.Entity<ApplicationUserGroup>()
                .HasKey(ug => new { ug.UserId, ug.GroupId });

            builder.Entity<ApplicationUserGroup>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId);

            builder.Entity<ApplicationUserGroup>()
                .HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId);

            // GroupPermission: Composite Key
            builder.Entity<GroupPermission>()
                .HasKey(gp => new { gp.GroupId, gp.PermissionId });

            builder.Entity<GroupPermission>()
                .HasOne(gp => gp.Group)
                .WithMany(g => g.GroupPermissions)
                .HasForeignKey(gp => gp.GroupId);

            builder.Entity<GroupPermission>()
                .HasOne(gp => gp.Permission)
                .WithMany(p => p.GroupPermissions)
                .HasForeignKey(gp => gp.PermissionId);

            // Permission: Primary Key
            builder.Entity<Permission>()
                .HasKey(p => p.PermissionId);

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Company)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CompanyId);

            builder.Entity<Ticket>()
                .HasOne(t => t.Company)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CompanyId);

            builder.Entity<Ticket>()
                .HasOne(t => t.CreatedBy)
                .WithMany(u => u.CreatedTickets)
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Ticket>()
                .HasOne(t => t.Owner)
                .WithMany(u => u.OwnedTickets)
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Ticket>()
                .HasOne(t => t.Status)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => t.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
