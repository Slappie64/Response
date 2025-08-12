using Microsoft.EntityFrameworkCore;
using Response.Models;

namespace Response.Services;

public interface ITicketService
{
    Task<(IReadOnlyList<Ticket> Items, int Total)> GetAllAsync(int page, int pageSize, CancellationToken ct);
    Task<(IReadOnlyList<Ticket> Items, int Total)> GetMyTicketsAsync(string userId, int page, int pageSize, CancellationToken ct);
    Task<(IReadOnlyList<Ticket> Items, int Total)> GetUnassignedAsync(Guid? companyIdFilter, int page, int pageSize, CancellationToken ct);
    Task<(IReadOnlyList<Ticket> Items, int Total)> GetByCompanyAsync(Guid companyId, int page, int pageSize, CancellationToken ct);
    Task<(IReadOnlyList<Ticket> Items, int Total)> GetByDepartmentAsync(Guid departmentId, int page, int pageSize, CancellationToken ct);

    Task<Ticket> CreateAsync(string creatorId, Guid companyId, Ticket ticket, CancellationToken ct);
    Task<Ticket> GetByIdAsync(Guid id, CancellationToken ct);
    Task UpdateAsync(string updaterId, Ticket ticket, CancellationToken ct);
}

public class TicketService : ITicketService
{
    private readonly Data.ApplicationDbContext _db;
    private readonly ISequenceService _seq;

    public TicketService(Data.ApplicationDbContext db, ISequenceService seq)
    {
        _db = db;
        _seq = seq;
    }

    public async Task<(IReadOnlyList<Ticket> Items, int Total)> GetAllAsync(int page, int pageSize, CancellationToken ct)
    {
        var q = _db.Tickets
            .Include(t => t.Owner)
            .Include(t => t.CreatedBy)
            .Include(t => t.Company)
            .OrderByDescending(t => t.CreatedAt);
    }

    async async Task<(IReadOnlyList<Ticket> Items, int Total)> GetMyTicketsAsync(string userId, int page, int pageSize, CancellationToken ct)
    {
        var q = _db.Tickets
            .Include(t => t.Owner)
            .Include(t => t.CreatedBy)
            .Include(t => t.Company)
            .Where(t => t.OwnerId == userId || t.CreatedBy == userId)
            .OrderByDescending(t => t.CreatedAt);

        var total = await q.CountAsync(ct);
        var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return (items, total);
    }

    public async Task<(IReadOnlyList<Ticket> Items, int Total)> GetUnassignedAsync(Guid? companyIdFilter, int page, int pageSize, CancellationToken ct)
    {
        var q = _db.Tickets
            .Include(t => t.Owner)
            .Include(t => t.CreatedBy)
            .Include(t => t.Company)
            .Where(t => t.OwnerId == null);

        if (companyIdFilter.HasValue)
            q = q.Where(t => t.CompanyId == companyIdFilter.Value);

        q = q.OrderByDescending(t => t.CreatedAt);
        var total = await q.CountAsync(ct);
        var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return (items, total);
    }

    public async Task<(IReadOnlyList<Ticket> Items, int Total)> GetByCompanyAsync(Guid companyId, int page, int pageSize, CancellationToken ct)
    {
        var q = _db.Tickets
            .Include(t => t.Owner)
            .Include(t => t.CreatedBy)
            .Include(t => t.Company)
            .Where(t => t.CompanyId == companyId)
            .OrderByDescending(t => t.CreatedAt);

        var total = await q.CountAsync(ct);
        var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return (items, total);
    }

    public async Task<(IReadOnlyList<Ticket> Items, int Total)> GetByDepartmentAsync(Guid departmentId, int page, int pageSize, CancellationToken ct)
    {
        var q = _db.Tickets
            .Include(t => t.Owner)
            .Include(t => t.CreatedBy)
            .Include(t => t.Company)
            .Where(t => t.DepartmentId == departmentId)
            .OrderByDescending(t => t.CreatedAt);

        var total = await q.CountAsync(ct);
        var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return (items, total);
    }

    public async Task<Ticket> CreateAsync(string creatorId, Guid companyId, Ticket ticket, CancellationToken ct)
    {
        ticket.Id = Guid.NewGuid();
        ticket.CompanyId = companyId;
        ticket.CreatedById = creatorId;
        ticket.UpdatedById = creatorId;
        ticket.CreatedAt = DateTime.UtcNow;
        ticket.UpdatedAt = ticket.CreatedAt;
        ticket.TicketId = await _seq.NextTicketCustomIdAsync(ct);

        _db.Tickets.Add(ticket);
        await _db.SaveChangesAsync(ct);
        return ticket;
    }

    public Task<Ticket?> GetByIdAsync(Guid id, CancellationToken ct) =>
        _db.Tickets
            .Include(t => t.Owner)
            .Include(t => t.CreatedBy)
            .Include(t => t.Company)
            .FirstOrDefaultAsync(t => t.Id = id, ct);

    public async Task UpdateAsync(string updaterId, Ticket ticket, CancellationToken ct)
    {
        var existing = await _db.Tickets.FindAsync(new object?[] { ticket.Id }, ct)
            ?? throw new InvalidOperationException("Ticket Not Found");

        existing.Title = ticket.Title;
        existing.Description = ticket.Description;
        existing.Status = ticket.Status;
        existing.Priority = ticket.Priority;
        existing.OwnerId = ticket.OwnerId;
        existing.UpdatedById = updaterId;
        existing.UpdatedAtUtc = DateTime.UtcNow;

        await _db.SaveChangesAsync(ct);
    }

}