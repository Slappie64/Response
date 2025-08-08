using System;
using System.Collections.Generic;

namespace Response.Data
{
    public class Ticket
    {
        // Primary key: uniquely identifies each ticket
        public int? Id { get; set; }

        // Brief title summarizing the ticket's issue or request
        public string? Title { get; set; }

        // Detailed description of the ticket's content or context
        public string? Description { get; set; }

         // Foreign key: ID of the user who owns the ticket
        public string? OwnerId { get; set; }

        // Navigation property to the ApplicationUser who owns the ticket
        public ApplicationUser? Owner { get; set; }

        // Foreign key: ID of the user who created the ticket
        public string? CreatorId { get; set; }

        // Navigation property to the ApplicationUser who created the ticket
        public ApplicationUser? Creator { get; set; }

        //Foreign key: ID of the company associated with this ticket
        public int? CompanyId { get; set; }

        // Navigation property to the Company entity related to this ticket
        public Company? Company { get; set; }

        // Foreign key: ID of the department related to this ticket
        public int? DepartmentId { get; set; }

        // Navigation property to the associated Department entity
        public Department? Department { get; set; }

        // Foreign key: ID of the current status of the ticket
        public int? StatusId { get; set; }

        // Navigation property to the Status object representing ticket state
        public Status? Status { get; set; }

        // Timestamp when the ticket was first created
        public DateTime? CreatedAt { get; set; }

        // Timestamp of the most recent update to the ticket
        public DateTime? UpdatedAt { get; set; }

        // List of tags assigned to the ticket (via join table)
        public ICollection<TicketTag>? TicketTags { get; set; }

        // Collection of comments linked to the ticket
        public ICollection<TicketComment>? Comments { get; set; }

        // List of attachments (files or media) associated with this ticket
        public ICollection<TicketAttachment>? Attachments { get; set; }
    }
}