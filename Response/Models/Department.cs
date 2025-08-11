namespace Response.Models;

public class Department
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default;

    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = default;

    public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
}