using Microsoft.AspNetCore.Identity;

namespace Response.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    // Immutable, human readable User ID
    public string UserId { get; set; } = default;

    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = default;

    public Guid? DepartmentId { get; set; }
    public Department Department { get; set; }

    public ICollection<Ticket> OwnedTickets { get; set; } = [];
    public ICollection<Ticket> CreatedTickets { get; set; } = [];
}

