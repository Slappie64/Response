namespace Response.Data;

public class SecurityGroup
{
    // 🔹 Security Group Information
    public int SecurityId { get; set; }
    public string Name { get; set; }

    // 🔗 Company Relation
    public ICollection<ApplicationUserGroup> UserGroups { get; set; }

    // 🔗 Group Permissions
    public ICollection<GroupPermission> GroupPermissions { get; set; }
}
