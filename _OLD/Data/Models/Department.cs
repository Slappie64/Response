namespace Response.Data
{
    public class Department
    {
        // Primary key – uniquely identifies each department
        public int Id { get; set; }

        // Optional name of the department (nullable string)
        // Example: "Finance", "HR", "Support"
        public string? Name { get; set; }
    }
}
