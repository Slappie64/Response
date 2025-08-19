using Response.Domain.Common;

namespace Response.Infrastructure.Persistence;

public class EmailTemplate : ITenantEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string TemplateType { get; set; } = default; // "Ticket Created", "Ticket Updated" etc
    public string Subject { get; set; } = default;
    public string BodyHtml { get; set; } = default;
    public DateTime UpdatedAtUTC { get; set; } = DateTime.UtcNow;
}