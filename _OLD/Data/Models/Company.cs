namespace Response.Data
{
    public class Company
    {
        // Primary key – uniquely identifies each department
        public int Id { get; set; }

        // Optional name of the department (nullable string)
        // Example: "Finance", "HR", "Support"
        public string? Name { get; set; }

        // Data for the company logo, stored as a byte array
        public byte[]? CompanyLogo { get; set; } 
    }
}
