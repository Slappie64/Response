using Response.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class TicketService : i_TicketService
{
    private readonly ApplicationDbContext _context;

    public TicketService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Ticket>> GetAllTicketsAsync() =>
        await _context.Tickets
            .Include(t => t.Title)
            //.Include(t => t.Description)
            //.Include(t => t.Priority);
            //.Include(t => t.DepartmentId)
            //.Include(t => t.CompanyId)
            //.Include(t => t.StatusId)
            //.Include(t => t.CreatorId);
            .ToListAsync();
}