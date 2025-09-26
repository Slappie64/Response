using System.ComponentModel.DataAnnotations;

namespace Response.Server.Models;

public class Tenant
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public virtual ICollection<ApplicationUser> Users { get; set; } = [];
    public virtual ICollection<Ticket> Tickets { get; set; } = [];
}