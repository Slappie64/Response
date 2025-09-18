using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Response.Server.Data;
using Response.Shared.DTOs;
using Response.Shared.Models;

namespace Response.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _db;
    private readonly IConfiguration _cfg;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ApplicationDbContext db,
        IConfiguration cfg)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _db = db;
        _cfg = cfg;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest req)
    {
        // Org/Dept resolution or creation
        var org = _db.Organisations.FirstOrDefault(o => o.Name == req.OrganisationName) ??
            (await _db.Organisations.AddAsync(new Organisation
            {
                Name = req.OrganisationName
            })).Entity;

        Department? dep = null;
        if (!string.IsNullOrWhiteSpace(req.DepartmentName))
        {
            dep = _db.Departments.FirstOrDefault(d => d.Name == req.DepartmentName && d.OrganisationId == org.Id) ??
                (await _db.Departments.AddAsync(new Department
                {
                    Name = req.DepartmentName,
                    OrganisationId = org.Id
                })).Entity;
        }
        await _db.SaveChangesAsync();

        var user = new ApplicationUser
        {
            UserName = req.Email,
            Email = req.Email,
            OrganisationId = org.Id,
            DepartmentId = dep?.Id
        };

        var result = await _userManager.CreateAsync(user, req.Password);
        if (!result.Succeeded)
            return BadRequest(string.Join("; ", result.Errors.Select(e => e.Description)));

        var role = req.IsAdmin ? "Admin" : "User";
        if (!await _roleManager.RoleExistsAsync(role))
            await _roleManager.CreateAsync(new IdentityRole(role));

        await _userManager.AddToRoleAsync(user, role);

        var token = await GenerateToken(user);
        return Ok(token);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest req)
    {
        var user = await _userManager.FindByEmailAsync(req.Email);
        if (user is null) return Unauthorized("Invalid Credentials");
        if (!await _userManager.CheckPasswordAsync(user, req.Password))
            return Unauthorized("Invalid Credentials");

        var token = await GenerateToken(user);
        return Ok(token);
    }

    private async Task<AuthResponse> GenerateToken(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!)
        };
        foreach (var role in roles) claims.Add(new Claim(ClaimTypes.Role role));

        // Include Org/Dept claims to inherit on ticket creation
        claims.Add(new Claim("org", user.OrganisationId.ToString()));
        if (user.DepartmentId.HasValue)
            claims.Add(new Claim("dep", user.DepartmentId.Value.ToString()));

        var jwt = _cfg.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTD8.GetBytes(jwt["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(jwt["ExpiresMinutes"]!)),
            signingCredentials: creds);

        return new AuthResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Email = user.Email!,
            Roles = roles.ToArray()
        };
    }
}