using Microsoft.AspNetCore.Identity;
using System.ComponentMode.DataAnnotations;

namespace Response.Server.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    public int? TenantId { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public virtual Tenant? Tenant { get; set; }
    public virtual ICollection<Ticket> CreatedTickets { get; set; } = [];
    public virtual ICollection<Ticket> AssignedTickets { get; set; } = [];

    public string FullName => $"{FirstName} {LastName}";
}