using System.Security.Claims;
using System.Threading.Tasks;

namespace Response.Infrastructure.Tenancy;

public interface IUserProvisioner
{
    Task ProvisionAsync(ClaimsPrincipal principal);
}