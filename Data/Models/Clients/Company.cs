namespace Response.Data
{
    public class Company
    {
        // 🔹 Company Information
        public int CompanyId { get; set; }
        public string? Name { get; set; }
        public string? CompanyLogo { get; set; }

        // 🔸 Contact Details
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }

        // 🏢 Company Relations
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
