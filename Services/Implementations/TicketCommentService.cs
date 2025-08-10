using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Response.Data;

namespace Response.Services
{
    public class TicketCommentService : ITicketCommentService
    {
        private readonly ApplicationDbContext _db;
        public TicketCommentService(ApplicationDbContext db) => _db = db;

        // Get all comments for a ticket
        public Task<IReadOnlyList<TicketComment>> GetCommentsByTicketIdAsync(int ticketId) =>
            _db.TicketComments.AsNoTracking()
                .Where(tc => tc.TicketId == ticketId)
                .ToListAsync()
                .ContinueWith(t => (IReadOnlyList<TicketComment>)t.Result);
        
        // Create New Comment
        public async Task<TicketComment?> CreateCommentAsync(TicketComment comment)
        {
            _db.TicketComments.Add(comment);
            await _db.SaveChangesAsync();
            return comment;
        }
    }

}