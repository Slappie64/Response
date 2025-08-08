using System.Collections.Generic;

namespace Response.Data
{
    public class Group
    {
        // Primary key: uniquely identifies each Group entity
        public int Id { get; set; } 

        // Optional name of the group (e.g. "Admins", "Support Team")
        public string? Name { get; set; } 

        // Navigation property linking users to this group (many-to-many via UserGroup)
        public ICollection<UserGroup>? UserGroups { get; set; } 

        // Navigation property for permissions assigned to this group
        public ICollection<GroupPermission>? Permissions { get; set; } 
    }
}