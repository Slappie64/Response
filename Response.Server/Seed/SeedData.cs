using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Response.Server.Data;
using Response.Server.Services;
using Response.Shared.Models;

namespace Response.Server.Seed;

public static class SeedData
{
    public static async Task Run(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await db.Database.MigrateAsync();

        var roles = new[] { "Admin", "User" };
        foreach (var r in roles)
        {
            if (!await roleMgr.RoleExistsAsync(r))
                await roleMgr.CreateAsync(new IdentityRole(r));
        }

        var org = await db.Organisations.FirstOrDefaultAsync() ??
            (await db.Organisations.AddAsync(new Organisation
            {
                Name = "Response MSP"
            })).Entity;

        var dep = await db.Departments.FirstOrDefaultAsync() ??
            (await db.Departments.AddAsync(new Department
            {
                Name = "Support"
            })).Entity;
        await db.SaveChangesAsync();

        var adminEmail = "admin@response.local";
        var admin = await userMgr.FindByEmailAsync(adminEmail);
        if (admin is null)
        {
            admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                OrganisationId = org.Id,
                DepartmentId = dep.Id
            };
            await userMgr.CreateAsync(admin, "ChangeMe!23#");
            await userMgr.AddToRoleAsync(admin, "Admin");
        }
    }
}