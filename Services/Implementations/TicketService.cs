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

    public async Task<IEnumerable<Ticket>> GetAllTicketsAsync() =>
        await _context.Tickets.ToListAsync();
}