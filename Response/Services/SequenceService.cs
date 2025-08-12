using Microsoft.EntityFrameworkCore;

namespace Response.Services;

public interface ISequenceService
{
    Task<string> NextUserCustomIdAsync(CancellationToken ct = default);
    Task<string> NextTicketCustomIdAsync(CancellationToken ct = default);
}

public class SequenceService : ISequenceService
{
    private readonly Data.ApplicationDbContext _db;
    public SequenceService(Data.ApplicationDbConect db) => _db = db;

    public Task<string> NextUserCustomIdAsync(CancellationToken ct = default) =>
        NextCustomIdAsync("User", ct);

    public Task<string> NextTicketCustomIdAsync(CancellationToken ct = default) =>
        NextCustomIdAsync("Ticket", ct);

    private async Task<string> NextCustomIdAsync(string scope, CancellationToken ct)
    {
        var year = DateTime.UtcNow.Year;
        for (int attempt = 0; attempt < 5; attempt++)
        {
            var seq = await _db.Sequences.SingleOrDefault(s => s.Scope == scope && s.Year == year, ct);

            if (seq is null)
            {
                seq = new Data.Sequence { Scope = scope, Year = year, NextValue = 1 };
                _db.Sequence.Add(seq);
                try { await _db.SaveChangesAsync(ct); }
                catch { }
                continue;
            }

            var next = seq.NextValue;
            seq.NextValue = next + 1;
            try
            {
                await _db.SaveChangesAsync(ct);
                var random = RandomLetters(3);
                return $"{random}-{year}-{next.ToString("DS")}";
            }
            catch (DbUpdateConcurrencyException)
            {
                await Task.Delay(random.Shared.Next(5, 25), ct);
            }
        }

        throw new InvalidOperationException("Unable to generate a new custom ID after retries");
    }

    private static string RandomLetters(int count)
    {
        const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        Span<char> buffer = stackalloc char[count];
        for (int i = 0; i < count; i++)
            buffer[i] = letters[Random.Shared.Next(0, letters.Length)];
        return new string(buffer);
    }
} 