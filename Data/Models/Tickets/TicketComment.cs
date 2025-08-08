namespace Response.Data;

public class TicketComment
{
    public int CommentId { get; set; }
    public string Content { get; set; }
    public DateTime PostedAt { get; set; }

    public int TicketId { get; set; }
    public Ticket Ticket { get; set; }

    public string AuthorUserId { get; set; }
    public ApplicationUser AuthorUser { get; set; }
}