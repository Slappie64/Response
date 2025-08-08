using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Response.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string JobTitle { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; } // URL to user's profile picture
    public ICollection<ApplicationUserGroup> UserGroups  { get; set; } = new List<ApplicationUserGroup>();
    public ICollection<Department> Departments { get; set; } = new List<Department>();
}

