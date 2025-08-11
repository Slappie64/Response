namespace Response.Models;

public class Company
{
    public Guid id { get; set; }
    public string Name { get; set; }

    public ICollection<Department> Departments { get; set; } = new List<Department>();
    public ICollecton<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
}