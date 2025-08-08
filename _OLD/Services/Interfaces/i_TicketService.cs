using Response.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public interface i_TicketService
{
    Task<IEnumerable<Ticket>> GetAllTicketsAsync();
    Task<IEnumerable<Company>> GetAllCompaniesAsync();
    Task<IEnumerable<Department>> GetAllDepartmentsAsync();
    Task<IEnumerable<Status>> GetAllStatusesAsync();
    //Task<Ticket> GetTicketByIdAsync(int id);
    //Task CreateTicketAsync(Ticket ticket);
    //Task UpdateTicketAsync(Ticket ticket);
    //Task DeleteTicketAsync(int id);
}