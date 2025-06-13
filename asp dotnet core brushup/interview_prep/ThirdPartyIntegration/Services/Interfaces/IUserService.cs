using interview_prep.Dto.UserDTO;
using interview_prep.Models;
using interview_prep.Models.user;
using interview_prep.Responses;

namespace interview_prep.Services.Interfaces;

public interface IUserService
{
    Task<OperationalResult<UserResponseDTO>> RegisterUserAsync(CreateUserDTO credentials);
    Task<OperationalResult<LoginResponseDto>> LoginUserAsync(LoginCredentialsDTO credentials);
    Task<OperationalResult<User>> GetUserByEmail(string email);
}