using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Response.Components;
using Response.Components.Account;
using Response.Data;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------------------
// 1. Register Blazor server‐side components and interactive modes
// ----------------------------------------------------------------
builder.Services.AddRazorComponents()       // Core Blazor components
    .AddInteractiveServerComponents();      // Enable interactive rendering on the server

// -------------------------------------------------------
// 2. Configure authentication state cascading and helpers
// -------------------------------------------------------
builder.Services.AddCascadingAuthenticationState();     
builder.Services.AddScoped<IdentityUserAccessor>();         // Helper to get current IdentityUser
builder.Services.AddScoped<IdentityRedirectManager>();      // Manages redirects after login/logout
builder.Services.AddScoped<AuthenticationStateProvider,
    IdentityRevalidatingAuthenticationStateProvider>();     // Keeps auth state in sync

// -------------------------------------------------------
// 3. Add ASP.NET Core Identity cookie schemes
// -------------------------------------------------------
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;        // Main identity cookie
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;     // External logins
    })
    .AddIdentityCookies();

// -------------------------------------------------------
// 4. Add MudBlazor Services
// -------------------------------------------------------
builder.Services.AddMudServices();

// -----------------------------------------------------------------
// 5. Configure Entity Framework Core with SQL Server connection
// -----------------------------------------------------------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Use SQL Server for EF Core

builder.Services.AddDatabaseDeveloperPageExceptionFilter();                                // Friendly EF error pages in development


// ----------------------------------------------------------
// 6. Configure IdentityCore and tie it to our user store
// ----------------------------------------------------------
builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true) // Force email confirmation
    .AddEntityFrameworkStores<ApplicationDbContext>()                                                       // Persist users in EF Core
    .AddSignInManager()                                                                                     // SignInManager for login workflows
    .AddDefaultTokenProviders();                                                                            // Tokens for password reset, email confirmation

// -----------------------------------------------------------------
// 7. Provide a no‐op email sender (replace in prod with real service)
// -----------------------------------------------------------------
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();


// -------------------------------------------
// 8. Configure the HTTP middleware pipeline
// -------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();    // Enable automatic EF Core migrations endpoint
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts(); // Enforce HTTP Strict Transport Security
}

app.UseHttpsRedirection(); // Redirect HTTP to HTTPS
app.UseAntiforgery();   // Validate antiforgery tokens on applicable requests

// ------------------------------------------
// 9. Map static assets and Blazor endpoints
// ------------------------------------------
app.MapStaticAssets();                  // Serve wwwroot files (CSS, JS, images)
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();  // Render the root component in interactive server mode

// -------------------------------------------
// 10. Map additional endpoints for Identity UI
// -------------------------------------------
app.MapAdditionalIdentityEndpoints();   // Routes for login, register, logout, etc.

app.Run();  // Start the web application
