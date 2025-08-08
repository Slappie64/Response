namespace Response.Data
{
    public class UserGroups
    {
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        public string GroupId { get; set; } = string.Empty;
        public UserGroup Group { get; set; } = null!;
    }
}