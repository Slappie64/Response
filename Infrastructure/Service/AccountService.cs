using Domain.DTO.Request;
using Domain.DTO.Response;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Service
{
    public class AccountService :IAccountService
    {
        private readonly SignInManager<User> signInManager;

        public AccountService(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        public Task<BaseResponse> RegisterUser(RegisterUserRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<string>> VerifyUser(string email, string password)
        {
            BaseResponse<string> response = new();

            var user = await signInManager.UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                response.ErrorMessage = "User is not found";
                response.IsSuccess = false;
                return response;
            }

            var result = await signInManager.UserManager.CheckPasswordAsync(user, password);
            response.IsSuccess = result;
            if (!result)
            {
                response.ErrorMessage = "Invalid Email / Password";
            } else
            {
                response.Value = user.UserName;
            }
        }
    }
}
