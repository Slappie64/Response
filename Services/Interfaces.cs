using Response.Data;

namespace Response.Services
{
    // User service interface
    public interface IUserService
    {
        Task<ApplicationUser?> GetUserByIdAsync(int userId);
        Task<IReadOnlyList<ApplicationUser>> GetAllAsync();
        Task<IReadOnlyList<ApplicationUser>> GetByCompanyAsync(int companyId);
        Task<IReadOnlyList<ApplicationUser>> GetByDepartmentAsync(int departmentId);
        Task<IReadOnlyList<ApplicationUser>> GetBySecurityGroupAsync(int securityGroupId);
    }

    // Department service interface
    public interface IDepartmentService
    {
        Task<Department> GetDepartmentByIdAsync(int departmentId);
        Task<IReadOnlyList<Department>> GetAllAsync();
    }

    // Security group service interface
    public interface ISecurityGroupService
    {
        Task<SecurityGroup> GetSecurityGroupByIdAsync(int securityGroupId);
    }

    // Permission service interface
    public interface IPermissionService
    {

    }

    // Company service interface
    public interface ICompanyService
    {

    }

    // Ticket service interfaces
    public interface ITicketService
    {

    }

    // Ticket comment service interface
    public interface ITicketCommentService
    {

    }

    // Ticket attachment service interface
    public interface ITicketAttachmentService
    {

    }
}