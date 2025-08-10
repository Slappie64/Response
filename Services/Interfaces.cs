using Response.Data;

namespace Response.Services
{
    // User service interface
    public interface IUserService
    {
        Task<IReadOnlyList<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser?> GetUserByIdAsync(string userId);
        Task<IReadOnlyList<ApplicationUser>> GetUserByCompanyAsync(int companyId);
    }

    // Department service interface
    public interface IDepartmentService
    {
        Task<IReadOnlyList<Department>> GetAllAsync();
        Task<Department> GetDepartmentByIdAsync(int departmentId);
        Task<Department?> GetDepartmentByNameAsync(string companyName);
    }

    // Company service interface
    public interface ICompanyService
    {
        Task<IReadOnlyList<Company>> GetAllAsync();
        Task<Company?> GetCompanyByIdAsync(int companyId);
        Task<Company?> GetCompanyByNameAsync(string companyName);
    }

    // Security group service interface
    public interface ISecurityGroupService
    {
        Task<IReadOnlyList<SecurityGroup>> GetAllAsync();
        Task<SecurityGroup> GetSecurityGroupByIdAsync(int securityId);
        Task<SecurityGroup?> GetSecurityGroupByNameAsync(string securityGroupName);
    }

    // Permission service interface
    public interface IPermissionService
    {
        Task<IReadOnlyList<Permission>> GetAllAsync();
        Task<Permission?> GetPermissionByIdAsync(int permissionId);
        Task<Permission?> GetPermissionByNameAsync(string permissionName);
    }

    // Ticket service interfaces
    public interface ITicketService
    {
        Task<IReadOnlyList<Ticket>> GetAllAsync();
        Task<Ticket?> GetTicketByIdAsync(int ticketId);
        Task<IReadOnlyList<Ticket?>> GetTicketsByOwnerAsync(string ownerId);
        Task<IReadOnlyList<Ticket?>> GetTicketByCreatorAsync(string createdById);
        Task<IReadOnlyList<Ticket?>> GetTicketsByCompanyAsync(int companyId);
        Task<IReadOnlyList<Ticket?>> GetTicketsByStatusAsync(int statusId);
        Task<IReadOnlyList<Ticket?>> GetTicketsByPriorityAsync(int priorityId);
        Task<Ticket?> CreateTicketAsync(Ticket ticket);
        Task<Ticket?> UpdateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(int ticketId);
    }

    // Ticket comment service interface
    public interface ITicketCommentService
    {
        Task<IReadOnlyList<TicketComment>> GetCommentsByTicketIdAsync(int ticketId);
        Task<TicketComment> CreateCommentAsync(TicketComment comment);
    }

    // Ticket attachment service interface
    public interface ITicketAttachmentService
    {
        Task<IReadOnlyList<TicketAttachment>> GetAttachmentsByTicketIdAsync(int ticketId);
        Task<TicketAttachment> CreateAttachmentAsync(TicketAttachment attachment);
    }
}