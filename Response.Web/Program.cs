
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

using Response.Infrastructure.Persistence;
using Response.Infrastructure.Tenancy;
using Response.Web.Auth;
using Response.Web.Services;
using Response.Web.Components;

var builder = WebApplication.CreateBuilder(args)

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureId"));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Policies.CanManageTenants, p => p.RequireRole(RoleNames.Admin));
    options.AddPolicy(Policies.CanCloseTicket, p => p.RequireRole(RoleNames.Admin, RoleNames.Agent));
});

builder.Services.AddDbContext<AppDbContext>(OptionsBuilderConfigurationExtensions =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddMudServices();

builder.Services.AddScoped<ITenantProvider, HttpTenantProvider>();
builder.Services.AddScoped<ITenantResolutionService, TenantResolutionService>();
builder.Services.AddScoped<IHumanIdGenerator, HumanIdGenerator>();

builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddScoped<IEmailService, GraphEmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.Use(async (ctx, next) =>
{
    // The HttpTenantProvider reads TenantId from claims and maps to DB Tenant
    await next();
});

app.MapBlazorHub();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
