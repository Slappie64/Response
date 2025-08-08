namespace Response.Data;

public class Ticket
{
    public int TicketId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // 🔹 Created By (who submitted the ticket)
    public string CreatedById { get; set; }
    public ApplicationUser CreatedBy { get; set; }

    // 🔸 Owner (who is responsible for resolving it)
    public string OwnerId { get; set; }
    public ApplicationUser Owner { get; set; }

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