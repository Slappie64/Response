using System;

namespace Response.Data
{
    public class TicketComment
    {
        // Primary key: uniquely identifies each comment on a ticket
        public int Id { get; set; } 

        // Foreign key: links this comment to its associated ticket
        public int TicketId { get; set; } 
        
        // Navigation property: allows access to the related Ticket entity via EF Core
        public Ticket? Ticket { get; set; } 
        
        // Foreign key: stores the ID of the user who authored the comment
        public string? AuthorId { get; set; } 
        
        // Navigation property: links to the ApplicationUser who wrote the comment
        public ApplicationUser? Author { get; set; } 
        
        // Optional text: the actual comment content left by the user
        public string? Content { get; set; } 
        
        // Timestamp: records when the comment was created for audit and sorting
        public DateTime CreatedAt { get; set; } 
    }
}