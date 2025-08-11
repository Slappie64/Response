namespace Response.Data;

// Concurrency-Safe Numeric counter per (Scope, Year)
public class Sequence
{
    public int Id { get; set; }
    public string Scope { get; set; } = default!; //eg. Ticket or User
    public int Year { get; set; }
    public int NextValue { get; set; }
    public byte[] RowVersion { get; set; } = default!;
}