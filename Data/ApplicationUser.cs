using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Response.Data;

// Extend the IdentityUser model with custom properties and relationships
public class ApplicationUser : IdentityUser
{
    // Collection of groups this user is a member of
    public ICollection<UserGroup>? UserGroups { get; set; }
}