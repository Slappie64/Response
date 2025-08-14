using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Response.Client; 
using Response.Client.Pages;
using Response.Components;
using Response.Components.Account;
using Response.Data;
using Response.Data.Seed;
using Response.Models;
using Response.Services;

var builder = WebApplication.CreateBuilder(args);

// MudBlazor
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// DB
var cs = builder.Configuration.GetConnectionString("DefaultConnection")
         ?? throw new InvalidOperationException("Connection string not found.");
builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    opts.UseSqlServer(cs));

// Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // enable later
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();



// App services
builder.Services.AddScoped<ISequenceService, SequenceService>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<ITicketService, TicketService>();

var app = builder.Build();

// Migrate & seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var seq = scope.ServiceProvider.GetRequiredService<ISequenceService>();
    await DataSeeder.InitializeAsync(db, roleMgr, userMgr, seq);
}


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

app.Run();

// ====================================================================================
/* Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
else
{
    app.UseMigrationsEndPoint();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
app.MapBlazorHub();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();


app.Run();
*/