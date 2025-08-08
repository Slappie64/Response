namespace Response.Data
{
    public class ApplicationUserGroup
    {
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        public int GroupId { get; set; } = 0;
        public SecurityGroup Group { get; set; } = null!;
    }
}