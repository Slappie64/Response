public class Permission
{
    public int PermissionId { get; set; }
    public string Name { get; set; }

    public ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();
}