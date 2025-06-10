using interview_prep.Dto.UserDTO;
using interview_prep.Models;
using interview_prep.Models.user;
using interview_prep.Repositories.Interfaces;
using interview_prep.Responses;
using interview_prep.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace interview_prep.Services.Service;

public class UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

    public async Task<OperationalResult<UserResponseDTO>> RegisterUserAsync(CreateUserDTO credentials)
    {
        var emailUser = await _userRepository.GetUserByEmailAsync(credentials.Email);
        var phoneUser = await _userRepository.GetUserByPhoneAsync(credentials.PhoneNumber);

        if (emailUser != null)
        {
            return OperationalResult<UserResponseDTO>.FailureResult(new[] { "Email already exists." });
        }

        if (phoneUser != null)
        {
            return OperationalResult<UserResponseDTO>.FailureResult(new[] { "Phone already exists." });
        }


        // prepare the user object for new user creation
        User user = new User()
        {
            Email = credentials.Email,
            PhoneNumber = credentials.PhoneNumber,
            UserName = credentials.UserName
        };

        // hash the password from the given credentials
        user.PasswordHash = _passwordHasher.HashPassword(user, credentials.Password);

        var registrationStatus = await _userRepository.Register(user);

        // now register the user in the database
        if (registrationStatus.Errors.Count > 0)
        {
            return OperationalResult<UserResponseDTO>.FailureResult(registrationStatus.Errors.ToArray());
        }

        // 5. Map the newly created user to a UserResponseDTO
        var userResponse = new UserResponseDTO
        {
            Id = user.Id, // Assuming Id is populated by the repository after successful save
            UserName = user.UserName,
        };

        return OperationalResult<UserResponseDTO>.SuccessResult(userResponse);
    }

    public async Task<OperationalResult<LoginResponseDto>> LoginUserAsync(LoginCredentialsDTO credentials)
    {
        // check if email exists
        var emailUser = await _userRepository.GetUserByEmailAsync(credentials.Email);

        if (emailUser == null)
        {
            return OperationalResult<LoginResponseDto>.FailureResult(new[] { "Email does not exist." });
        }

        var passwordVerification =
            _passwordHasher.VerifyHashedPassword(emailUser, emailUser.PasswordHash, credentials.Password);

        // match the password
        if (passwordVerification == PasswordVerificationResult.Failed)
        {
            return OperationalResult<LoginResponseDto>.FailureResult(new[] { "The password did not match." });
        }

        var loginResponse = new LoginResponseDto()
        {
            Message = "You have successfully logged in."
        };
        
        return OperationalResult<LoginResponseDto>.SuccessResult(loginResponse);
    }

    public async Task<OperationalResult<User>> GetUserByEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return OperationalResult<User>.FailureResult(new [] {"Email cannot be empty."});
        }

        // 1. Delegate to the repository to get the entity
        var user = await _userRepository.GetUserByEmailAsync(email);

        if (user == null)
        {
            return  OperationalResult<User>.FailureResult(new [] {$"User with email '{email}' not found."});
        }
        
        return OperationalResult<User>.SuccessResult(user);
    }
}