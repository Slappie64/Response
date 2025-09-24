using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Response.Server.Data;
using Response.Server.Services;
using Response.Shared.DTOs;
using Response.Shared.Models;

namespace Response.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TicketController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly ITicketReferenceGenerator _refGen;

    public TicketController(ApplicationDbContext db, ITicketReferenceGenerator refGen)
    {
        _db = db;
        _refGen = refGen;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketResponse>>> GetAll()
    {
        var tickets = await _db.Tickets
            .Include(t => t.Creator)
            .Include(t => t.Owner)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        var res = tickets.Select(t => new TicketResponse
        {
            Id = t.Id,
            Reference = t.Reference,
            Name = t.Name,
            Description = t.Description,
            Status = t.Status,
            Priority = t.Priority,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt,
            CreatorEmail = t.Creator.Email,
            OwnerEmail = t.Owner?.Email
        });

        return Ok(res);
    }

    [HttpPost]
    public async Task<ActionResult<TicketResponse>> Create([FromBody] TicketCreateRequest req)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var orgClaim = User.FindFirstValue("org");
        var depClaim = User.FindFirstValue("dep");

        var reference = await _refGen.NextAsync();

        var ticket = new Ticket
        {
            Reference = reference,
            Name = req.Name,
            Description = req.Description,
            Priority = req.Priority,
            CreatorId = userId,
            OwnerId = req.OwnerId,
            OrganisationId = Guid.Parse(orgClaim),
            DepartmentId = string.IsNullOrWhiteSpace(depClaim) ? null : Guid.Parse(depClaim)
        };

        _db.Tickets.Add(ticket);
        await _db.SaveChangesAsync();


        // Reload For Response
        ticket = await _db.Tickets
            .Include(t => t.Creator)
            .Include(t => t.Owner)
            .FirstAsync(t => t.Id == ticket.Id);

        var res = new TicketResponse
        {
            Id = ticket.Id,
            Reference = ticket.Reference,
            Name = ticket.Name,
            Description = ticket.Description,
            Status = ticket.Status,
            Priority = ticket.Priority,
            CreatedAt = ticket.CreatedAt,
            UpdatedAt = ticket.UpdatedAt,
            CreatorEmail = ticket.Creator.Email,
            OwnerEmail = ticket.Owner?.Email
        };

        return Ok(res);
    }
}

