using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Response.Models;
using Response.Services;

namespace Response.Data.Seed;

public static class DataSeeder
{
    public static async Task InitializeAsync(
        ApplicationDbContext db,
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager,
        ISequenceService seq)
    {
        await db.Database.MigrateAsync();

        //Roles
        foreach (var role in new[] { "Admin", "User" })
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

        // Company & Department
        var company = await db.Companies.FirstOrDefaultAsync(c => c.Name == "ResponseDemo")
            ?? (await db.Companies.AddAsync(new Company { id = Guid.NewGuid(), Name = "ResponseDemo" })).Entity;

        var department = await db.Departments.FirstOrDefaultAsync(d => d.CompanyId == company.id && d.Name == "IT Support")
            ?? (await db.Departments.AddAsync(new Department { Id = Guid.NewGuid(), CompanyId = company.id, Name = "IT Support" })).Entity;

        await db.SaveChangesAsync();

        // Admin User
        var adminEmail = "admin@response.local";
        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin is null)
        {
            var userId = await seq.NextUserCustomIdAsync();
            admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                CompanyId = company.id,
                DepartmentId = department.Id,
                UserId = userId
            };
            var result = await userManager.CreateAsync(admin, "Admin!2345"); // DEV ONLY
            if (!result.Succeeded)
                throw new Exception(string.Join(";", result.Errors.Select(e => e.Description)));


            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}