namespace Response.Data;

public class SecurityGroup
{
    public int SecurityId { get; set; }
    public string Name { get; set; }

    public ICollection<ApplicationUserGroup> UserGroups { get; set; }
    public ICollection<GroupPermission> GroupPermissions { get; set; }
}
