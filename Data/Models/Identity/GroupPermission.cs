namespace Response.Data;

public class GroupPermission
{
    public int GroupId { get; set; }
    public SecurityGroup Group { get; set; } = null!;

    public int PermissionId { get; set; }
    public Permission Permission { get; set; } = null!;
}