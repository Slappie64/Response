using Response.Domain.Entities;

namespace Response.Infrastructure.Persistence;

public static class DbInitializer
{
    public static void Seed(AppDbContext db)
    {
        if (!db.Tenants.Any())
        {
            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "Response",
                Domain = "response.local"
            };
            db.Tenants.Add(tenant);

            db.Users.Add(new AppUser
            {
                Id = Guid.NewGuid(),
                Tenant = tenant,
                DisplayName = "Admin User",
                Email = "admin@response.local",
                EntraObjectId = "00000000-0000-0000-0000-000000000000",
                Role = "Admin"
            });

            db.SaveChanges();
        }
    }
}