using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using MudBlazor.Services;

using Response.Infrastructure.Persistence;
using Response.Infrastructure.Tenancy;
using Response.Server.Auth;
using Response.Client.Pages;
using Response.Components;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var config = builder.Configuration;

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// Entra ID Authentication
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(config.GetSection("AzureAd"))
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddInMemoryTokenCaches();

// Authorization Policies (Enforcement later)
builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(Policies.CanManageTenants, p => p.RequireRole(RoleNames.Admin));
        options.AddPolicy(Policies.CanCloseTickets, p => p.RequireRole(RoleNames.Admin, RoleNames.Agent));
    });

// EF Core SQL Server (Local)
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));

// Tenancy and Provisioning
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenantProvider, CurrentTenant>();
builder.Services.AddScoped<IUserProvisioner, UserProvisioner>();
builder.Services.AddScoped<IClaimsTransformation, AppRoleClaimsTransformer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Response.Client._Imports).Assembly);

// Automigrate and seed database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    DbInitializer.Seed(db);
}

// Per Request Tenant and User Setup
app.Use(async (ctx, next) =>
{
    if (ctx.User?.Identity?.IsAuthenticated == true)
    {
        var tenant = ctx.RequestServices.GetRequiredService<ITenantProvider>();
        await tenant.EnsureTenantAsync(ctx.User);

        var provisioner = ctx.RequestServices.GetRequiredService<IUserProvisioner>();
        await provisioner.ProvisionAsync(ctx.User);
    }
    await next();
});


app.Run();
