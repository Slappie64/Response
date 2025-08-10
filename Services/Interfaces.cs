using Response.Data;

namespace Response.Services
{
    // User service interface
    public interface IUserService
    {
        Task<IReadOnlyList<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser?> GetUserByIdAsync(int userId);
        Task<IReadOnlyList<ApplicationUser>> GetUserByCompanyAsync(int companyId);
        Task<IReadOnlyList<ApplicationUser>> GetUserByDepartmentAsync(int departmentId);
        Task<IReadOnlyList<ApplicationUser>> GetUserBySecurityGroupAsync(int securityGroupId);
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
        Task<SecurityGroup> GetSecurityGroupByIdAsync(int securityGroupId);
        Task<SecurityGroup?> GetSecurityGroupByNameAsync(string securityGroupName);
    }

    // Permission service interface
    public interface IPermissionService
    {
        Task<IReadOnlyList<Permission>> GetByGroupAsync(int groupId);
        Task<IReadOnlyList<Permission>> GetByUserAsync(string userId);
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
        Task<IReadOnlyList<Ticket?>> GetTicketsByDepartmentAsync(int departmentId);

        Task<Ticket?> CreateTicketAsync(Ticket ticket);
        Task<Ticket?> UpdateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(int ticketId);
    }

    // Ticket comment service interface
    public interface ITicketCommentService
    {
        Task<IReadOnlyList<TicketComment>> GetCommentsByTicketIdAsync(int ticketId);
        Task<TicketComment> CreateAsync(TicketComment comment);
    }

    // Ticket attachment service interface
    public interface ITicketAttachmentService
    {
        Task<IReadOnlyList<TicketAttachment>> GetByTicketAsync(int ticketId);
        Task<TicketAttachment> CreateAsync(TicketAttachment attachment);
    }
}