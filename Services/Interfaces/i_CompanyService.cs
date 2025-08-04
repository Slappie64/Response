using Response.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public interface i_CompanyService
{
    Task<List<Company>> GetAllCompaniesAsync();
}