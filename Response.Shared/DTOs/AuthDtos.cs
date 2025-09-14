namespace Response.Shared.DTOs
{
    
}

public class RegisterRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string OrganisationName { get; set; } = default!;
    public string? DepartmentName { get; set; }
    public bool IsAdmin { get; set; } = false;
}

public class LoginRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class AuthResponse
{
    public string Token { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string[] Roles { get; set; } = [];
}
