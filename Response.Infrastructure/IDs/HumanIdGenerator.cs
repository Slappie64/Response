using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Response.Infrastructure.Tenancy;

namespace Response.Infrastructure.Persistence;

public class HumanIdGenerator : IHumanIdGenrator
{
    private readonly AppDbContext _db;
    private readonly ITenantProvider _tenant;

    public HumanIdGenerator(AppDbContext db, ITenantProvider tenant)
    {
        _db = db;
        _tenant = tenant;
    }

    public async Task<string> NextAsync(string entityName, CancellationToken ct = default)
    {
        if (_tenant.CurrentTenantId is null || string.IsNullOrWhiteSpace(_tenant.TenantCode))
            throw new InvalidOperationException("Tenant not resolved.");

        var tenantId = _tenant.CurrentTenantId!.Value;
        long next;

        // Concurrency safe increment with row-level lock
        await using var tx = await _db.Database.BeginTransactionAsync(ct);
        // Ensure row exists
        await _db.Database.ExecuteSqlRawAsync(
            """
            IF NOT EXISTS (SELECT 1 FROM TenantSequences WITH (UPDLOCK, HOLDLOCK) WHERE TenantId = {0} AND [Entity] = {1})
            INSERT INTO TenanSequences (TenantId, [Entity], LastValue) VALUES ({0}, {1}, 0)
            """,
            tenantId, entityName);

        await using var cmd = _db.Database.GetDbConnection().CreateCommand();
        cmd.CommandText = """
            UPDATE TenantSequences WITH (ROWLOCK, UPDLOCK)
                SET LastValue = LastValue + 1
            OUTPUT inserted.LastValue
            WHERE TenantId = @tenant AND [Entity] = @entity;
            """;

        cmd.Parameters.Add(new SqlParameter { "@tenant", tenandId });
        cmd.Parameters.Add(new SqlParameter { "@entity", entityName });

        if (cmd.Connection!.State != System.Data.ConnectionState.Open)
            await cmd.Connection.OpenAsync(ct);

        var prefix = entityName switch
        {
            "Ticket" => "TCK",
            _ => entityName.ToUpperInvariant()
        };
        return $"{prefix}-{_tenant.TenandCode}-{next.ToString("D6")}";

    }
}