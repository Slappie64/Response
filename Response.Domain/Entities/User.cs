using Microsoft.AspNetCore.Identity;

namespace Response.Domain.Entities;

public class User : IdentityUser
{
    public string? Avatar { get; set; } = string.Empty;
    public bool AccountConfirmed { get; set; } = false;

    public string? Company { get; set; } = string.Empty;
    public string? Department { get; set; } = string.Empty;
}