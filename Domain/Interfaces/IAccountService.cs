using Domain.DTO.Request;
using Domain.DTO.Response;

namespace Domain.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<string>> VerifyUser(string email, string password);
        Task<BaseResponse> RegisterUser(RegisterUserRequest request);
    }
}
