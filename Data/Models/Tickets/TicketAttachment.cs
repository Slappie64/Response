namespace Response.Data;
public class TicketAttachment
    {
        public int AttachmentId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; } // Or use a blob reference / storage URI
        public DateTime UploadedAt { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }