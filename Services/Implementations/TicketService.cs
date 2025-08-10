using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Response.Data;

namespace Response.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _db;
        public TicketService(ApplicationDbContext db) => _db = db;

        // Get all Tickets
        public Task<IReadOnlyList<Ticket>> GetAllAsync() =>
            _db.Tickets.AsNoTracking().ToListAsync().ContinueWith(t => (IReadOnlyList<Ticket>)t.Result);

        // Get Ticket by ID
        public Task<Ticket?> GetTicketByIdAsync(int ticketId) =>
            _db.Tickets.AsNoTracking().FirstOrDefaultAsync(t => t.TicketId == ticketId);

        // Get Tickets by Owner
        public Task<IReadOnlyList<Ticket?>> GetTicketsByOwnerAsync(string ownerId) =>
            _db.Tickets.AsNoTracking().Where(t => t.OwnerId == ownerId).ToListAsync().ContinueWith(t => (IReadOnlyList<Ticket?>)t.Result);

        // Get Tickets by Creator
        public Task<IReadOnlyList<Ticket?>> GetTicketByCreatorAsync(string createdById) =>
            _db.Tickets.AsNoTracking().Where(t => t.CreatedById == createdById).ToListAsync().ContinueWith(t => (IReadOnlyList<Ticket?>)t.Result);

        // Get Tickets by Company
        public Task<IReadOnlyList<Ticket?>> GetTicketsByCompanyAsync(int companyId) =>
            _db.Tickets.AsNoTracking().Where(t => t.CompanyId == companyId).ToListAsync().ContinueWith(t => (IReadOnlyList<Ticket?>)t.Result);

        // Get Tickets by Status
        public Task<IReadOnlyList<Ticket?>> GetTicketsByStatusAsync(int statusId) =>
            _db.Tickets.AsNoTracking().Where(t => t.StatusId == statusId).ToListAsync().ContinueWith(t => (IReadOnlyList<Ticket?>)t.Result);

        // Get Tickets by Priority
        public Task<IReadOnlyList<Ticket?>> GetTicketsByPriorityAsync(int priorityId) =>
            _db.Tickets.AsNoTracking().Where(t => t.PriorityId == priorityId).ToListAsync().ContinueWith(t => (IReadOnlyList<Ticket?>)t.Result);

        // Get Tickets by Department
        public Task<IReadOnlyList<Ticket?>> GetTicketsByDepartmentAsync(int departmentId)
            => _db.Tickets.AsNoTracking().Where(t => t.DepartmentId == departmentId).ToListAsync().ContinueWith(t => (IReadOnlyList<Ticket?>)t.Result);

        // Create a new Ticket
        public async Task<Ticket?> CreateTicketAsync(Ticket ticket)
        {
            _db.Tickets.Add(ticket);
            await _db.SaveChangesAsync();
            return ticket;
        }

        // Update an existing Ticket
        public async Task<Ticket?> UpdateTicketAsync(Ticket ticket)
        {
            _db.Tickets.Update(ticket);
            await _db.SaveChangesAsync();
            return ticket;
        }

        // Delete a Ticket
        public async Task DeleteTicketAsync(int ticketId)
        {
            var ticket = await _db.Tickets.FindAsync(ticketId);
            if (ticket != null)
            {
                _db.Tickets.Remove(ticket);
                await _db.SaveChangesAsync();
            }
        }
    }
}