namespace Response.Data
{
    public class GroupPermission
    {
        // Primary key – uniquely identifies each permission record
        public int Id { get; set; }

        // Foreign key to the Group this permission belongs to
        public int GroupId { get; set; }

        // Navigation property for EF Core – links to the related Group entity
        public Group? Group { get; set; }

        // Optional string representing a named permission
        // Example values: "Ticket.Create", "Admin.User.Edit"
        // Useful for role-based access control (RBAC) or scoped feature toggling
        public string? Permission { get; set; }
    }
}