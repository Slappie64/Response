namespace Response.Data;

public class Permission
{
    // 🔹 Permission Information
    public int PermissionId { get; set; }
    public string Name { get; set; }

    // 🔗 Group Permissions
    public ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();
}