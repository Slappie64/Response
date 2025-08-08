namespace Response.Data
{
    public class ApplicationUserGroup
    {
        // 🔹 UserGroup Information
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        // 🔗 Group Relation
        public int GroupId { get; set; } = 0;
        public SecurityGroup Group { get; set; } = null!;
    }
}