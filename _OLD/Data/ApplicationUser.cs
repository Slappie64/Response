using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Response.Data {
  public class ApplicationUser : IdentityUser
  {
    public ICollection<UserGroup>? UserGroups { get; set; }

    public ICollection<Ticket>? CreatedTickets { get; set; }
    
    public ICollection<Ticket>? OwnedTickets { get; set; }
  }
}