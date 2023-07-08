using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services.DTOs.Account;
using Services.Helpers.Responses;

namespace Services.Services.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterResponse> SignUpAsync(RegisterDto model);
        Task<string?> SignInAsync(LoginDto model);
        Task CreateRoleAsync();
        Task<IEnumerable<IdentityRole>> GetRolesAsync();
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task AddRoleToUserAsync(UserRoleDto model);
        Task<IEnumerable<string>> GetRolesByUserAsync(string userId);




    }
}
