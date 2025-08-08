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

    public async Task<IEnumerable<Company>> GetAllCompaniesAsync() =>
        await _context.Company.ToListAsync();

    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync() =>
        await _context.Departments.ToListAsync();
    
    public async Task<IEnumerable<Status>> GetAllStatusesAsync() =>
        await _context.Statuses.ToListAsync();

}