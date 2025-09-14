using System;
using Microsoft.AspNetCore.Identity;

namespace Response.Shared.Models;

public class ApplicationUser : IdentityUser
{
    public Guid OrganisationId { get; set; }
    public Guid? DepartmentId { get; set; }
    public Organisation Organisation { get; set; } = default!;
    public Department? Department { get; set; }
}