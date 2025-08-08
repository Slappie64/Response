namespace Response.Data
{
    public class UserGroup
    {
        // Foreign key: ID of the user in the group
        public string? UserId { get; set; }

        // Navigation property to the ApplicationUser entity
        public ApplicationUser? User { get; set; }

        // Foreign key: ID of the group the user belongs to
        public int? GroupId { get; set; }

        // Navigation property to the Group entity
        public Group? Group { get; set; }
    }
}