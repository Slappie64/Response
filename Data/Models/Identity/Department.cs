namespace Response.Data;

public class Department
{
    // Primary key: uniquely identifies each department record
    public int DepartmentId { get; set; }

    // Name of the department
    public string Name { get; set; } = string.Empty;

    // Description of the department's function or role
    public string Description { get; set; } = string.Empty;

    // 🔗 Relation to ApplicationUser
    public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
}