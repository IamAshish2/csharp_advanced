using interview_prep.Dto.UserDTO;
using interview_prep.Models.user;
using interview_prep.Responses;

namespace interview_prep.Repositories.Interfaces;

public interface IUserRepository
{
    Task<bool> SaveChangesAsync();
    Task<bool> CreateUserAsync(User user);
    Task<bool> DeleteUserAsync(string userId);
    Task<bool> UpdateUserAsync(User user);
    Task<List<GetUserDetailsDTO>> GetUserDetailsByIdAsync(string userId);
    Task<GetUserDetailsDTO> GetUserDetailsByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(string userId);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByPhoneAsync(string phone);
    Task<OperationalResult> Register(User user);
    Task<bool> ValidateRegistrationCredentials(CreateUserDTO credentials);
    Task<bool> UploadUserImage(ProfilePhoto profilePhoto);
}