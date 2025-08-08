using Response.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class UserService : i_UserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync() =>
        await _context.Users.ToListAsync();
}