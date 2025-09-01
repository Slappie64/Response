using Microsoft.AspNetCore.Mvc;
using Response.Infrastructure.Tenancy;

[ApiController]
[Route("api/tenant")]
public class TenantController : ControllerBase
{
    private readonly ITenantProvider _tenantProvider;
    public TenantController(ITenantProvider tenantProvider)
    {
        _tenantProvider = tenantProvider;
    }

    [HttpGet]
    public ActionResult<TenantInfoDTO> GetTenant()
    {
        if (_tenantProvider.TenantId is null)
            return NotFound("No tenant associated with the current user.");
        
        return new TenantInfoDTO
        {
            Id = _tenantProvider.Tenant!.Id,
            Name = _tenantProvider.Tenant.Name,
            EntraTenantId = _tenantProvider.Tenant.EntraTenantId
        };
    }
}