namespace Response.Domain.Entities;

public class Tenant
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Domain { get; set; }
    public ICollection<AppUser> Users { get; set; } = [];
    public ICollection<Ticket> Tickets { get; set; } = [];
}