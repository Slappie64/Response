using System;

namespace Response.Data
{
    public class TicketAttachment
    {
        // Primary key: uniquely identifies each attachment
        public int Id { get; set; } 

        // Foreign key linking this attachment to a specific ticket
        public int TicketId { get; set; } 

        // Navigation property for accessing the related ticket entity
        public Ticket? Ticket { get; set; } 

        // Name of the uploaded file (e.g. "error-log.txt")
        public string? FileName { get; set; } 

        // MIME type of the file (e.g. "application/pdf", "image/png")
        public string? ContentType { get; set; } 

        // Binary content of the file stored directly in the database
        public byte[]? Data { get; set; } 

        // Timestamp marking when the attachment was uploaded
        public DateTime UploadedAt { get; set; } 
    }
}