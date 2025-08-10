using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Response.Data;

namespace Response.Services
{
    public class TicketAttachmentService : ITicketAttachmentService
    {
        private readonly ApplicationDbContext _db;
        public TicketAttachmentService(ApplicationDbContext db) => _db = db;

        // Get all attachments for a ticket
        public Task<IReadOnlyList<TicketAttachment>> GetAttachmentsByTicketIdAsync(int ticketId) =>
            _db.TicketAttachments.AsNoTracking()
                .Where(ta => ta.TicketId == ticketId)
                .ToListAsync()
                .ContinueWith(t => (IReadOnlyList<TicketAttachment>)t.Result);
                
        // Create New Attachment
        public async Task<TicketAttachment?> CreateAttachmentAsync(TicketAttachment attachment)
        {
            _db.TicketAttachments.Add(attachment);
            await _db.SaveChangesAsync();
            return attachment;
        }
    }
}