namespace Response.Data;

public class GroupPermission
{
    // 🔹 Group Permission Information
    public int GroupId { get; set; }
    public SecurityGroup Group { get; set; } = null!;

    // 🔗 Permission Relation
    public int PermissionId { get; set; }
    public Permission Permission { get; set; } = null!;
}