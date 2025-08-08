using Response.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public interface i_UserService
{
    Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();

}