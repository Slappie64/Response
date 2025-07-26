using System;
using System.Collections.Generic;

namespace Response.Data
{
    public class Ticket
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public string? CreatorId { get; set; }
        public ApplicationUser? Creator { get; set; }

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public int? StatusId { get; set; }
        public Status? Status { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<TicketTag>? TicketTags { get; set; }
        public ICollection<TicketComment>? Comments { get; set; }
        public ICollection<TicketAttachment>? Attachments { get; set; }
    }
}