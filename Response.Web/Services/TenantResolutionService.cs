using Response.Infrastructure.Tenancy;

namespace Response.Web.Services;

public interface ITenantResolutionService
{
    Task EnsureResolvedAsync();
}

public class TenantResolutionService : ITenantResolutionService
{
    private readonly ITenantProvider _provider;
    public TenantResolutionService(ITenantProvider provider) => _provider = provider;
    public Task EnsureResolvedAsync() =>
        (_provider as HttpTenantProvider)?.EnsureResolvedAsync() ?? Task.CompletedTask;
}