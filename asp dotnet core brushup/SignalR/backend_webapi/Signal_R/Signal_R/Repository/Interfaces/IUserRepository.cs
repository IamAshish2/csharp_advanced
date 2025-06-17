using Microsoft.AspNetCore.Identity;
using Signal_R.Models;
using System.Security.Claims;

namespace Signal_R.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityUser> LoginWithGoogle(ClaimsPrincipal? claimsPrincipal);
        Task<ICollection<IdentityUser>> GetUsersAsync();
        //Task<GetUserDto> GetUserByIdAsync(string userId);
        Task<IdentityUser> GetUserByNameAsync(string userName);
        Task<IdentityUser> GetUserByEmailAsync(string email);
        Task<bool> CreateUserAsync(User newUser);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(string UserName);
        Task<bool> ChangePasswordAsync(string email, string password);
        Task<bool> isEmailInUse(string Email);
        Task<bool> isUserNameInUse(string userName);
        Task<bool> checkIfUserExists(string UserName);
        Task<bool> SaveAsync();
    }
}
