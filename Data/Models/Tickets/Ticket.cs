namespace Response.Data;

public class Ticket
{
    public int TicketId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // 🔹 Created By (who submitted the ticket)
    public int CreatedById { get; set; }
    public User CreatedBy { get; set; }

    // 🔸 Owner (who is responsible for resolving it)
    public int OwnerId { get; set; }
    public User Owner { get; set; }

    // 🏢 Company Relation
    public int CompanyId { get; set; }
    public Company Company { get; set; }

    // 📌 Status & Priority
    public int StatusId { get; set; }
    public TicketStatus Status { get; set; }

    public int PriorityId { get; set; }
    public TicketPriority Priority { get; set; }

    // 💬 Comments & Attachments
    public ICollection<TicketComment> Comments { get; set; }
    public ICollection<TicketAttachment> Attachments { get; set; }
}