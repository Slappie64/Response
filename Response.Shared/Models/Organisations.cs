using System;
using System.Collections.Generic;

namespace Response.Shared.Models;

public class Organisation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public ICollection<Department> Departments { get; set; } = [];
}

public class Department
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public Guid OrganisationId { get; set; }
    public Organisation Organisation { get; set; } = default!;
}