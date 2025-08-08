// Required namespaces for Blazor, Identity, EF Core, and MudBlazor
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Response.Components;
using Response.Components.Account;
using Response.Data;

// Create a WebApplication builder instance
var builder = WebApplication.CreateBuilder(args);

// 🔧 Add MudBlazor UI services to the DI container
builder.Services.AddMudServices();

// 🧩 Register Razor components with interactive server-side rendering
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 🔐 Set up authentication state management for Blazor components
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>(); // Custom accessor for Identity user
builder.Services.AddScoped<IdentityRedirectManager>(); // Handles redirect logic for auth flows
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>(); // Revalidates user identity periodically

// 🔐 Configure authentication schemes and cookie handling
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies(); // Adds cookie-based authentication for Identity

// 🗄️ Configure EF Core with SQLite using connection string from config
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🛠️ Adds detailed exception page for EF Core during development
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// 👤 Configure IdentityCore with custom ApplicationUser and EF store
builder.Services.AddIdentityCore<ApplicationUser>(options => 
        options.SignIn.RequireConfirmedAccount = true) // Require email confirmation
    .AddEntityFrameworkStores<ApplicationDbContext>() // Use EF Core for user store
    .AddSignInManager() // Adds SignInManager for login operations
    .AddDefaultTokenProviders(); // Enables token generation for password reset, etc.

// 📧 Register a no-op email sender (can be replaced with actual implementation)
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

// 🚀 Build the app
var app = builder.Build();

// 🌐 Configure middleware pipeline based on environment
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint(); // Auto-applies migrations during development
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true); // Custom error page
    app.UseHsts(); // Enforces HTTPS with HTTP Strict Transport Security
}

// Redirect HTTP to HTTPS
app.UseHttpsRedirection(); 

// Protects against CSRF attacks
app.UseAntiforgery(); 

// 📦 Serve static assets (CSS, JS, etc.)
app.MapStaticAssets();

// 🧩 Map Razor components and enable interactive rendering
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

// 🔐 Map Identity endpoints (e.g., login, register, logout)
app.MapAdditionalIdentityEndpoints();

// 🏁 Start the application
app.Run();
