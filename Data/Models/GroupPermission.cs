namespace Response.Data
{
    public class GroupPermission
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public Group? Group { get; set; }
        public string? Permission { get; set; } // e.g. "Ticket.Create", "Admin.User.Edit"
    }
}