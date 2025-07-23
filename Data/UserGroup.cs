namespace Response.Data.Models
{
    public class UserGroup
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}