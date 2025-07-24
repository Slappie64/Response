using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Response.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public ICollection<UserGroup>? UserGroups { get; set; }
}

