using System;

namespace Response.Data.Models
{
    public class TicketAttachment
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}