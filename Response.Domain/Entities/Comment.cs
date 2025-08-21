namespace Response.Domain.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public Guid TicketId { get; set; }
    public Ticket Ticket { get; set; } = default!;
    public Guid AuthorId { get; set; }
    public AppUser Author { get; set; } = default!;
    public string Body { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}