namespace Response.Data
{
    public class Status
    {
        // Primary key: uniquely identifies each status record (e.g. 1 = New, 2 = Closed)
        public int Id { get; set; }

        // Optional status label used to categorize ticket stages (e.g. "New", "Open", "Resolved", "Closed")
        public string? Name { get; set; }
    }
}