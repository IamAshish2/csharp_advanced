using interview_prep.Context;
using interview_prep.Dto.UserDTO;
using interview_prep.Models.user;
using interview_prep.Repositories.Interfaces;
using interview_prep.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace interview_prep.Repositories.Repository;

public class UserRepository(ApplicationDbContext context, IPasswordHasher<User> passwordHasher) : IUserRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        return await SaveChangesAsync();
    }

    public async Task<bool> DeleteUserAsync(string userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            _context.Users.Remove(user);
        }

        return await SaveChangesAsync();
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        return await SaveChangesAsync();
    }

    public async Task<List<GetUserDetailsDTO>> GetUserDetailsByIdAsync(string userId)
    {
        return await _context.Users
            .Where(u => u.Id == userId)
            .Select(u => new GetUserDetailsDTO()
            {
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            })
            .ToListAsync();
    }

    public async Task<GetUserDetailsDTO> GetUserDetailsByEmailAsync(string email)
    {
        return await _context.Users
            .Where(u => u.Email == email)
            .Select(u => new GetUserDetailsDTO()
            {
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            }).FirstOrDefaultAsync();
    }

    public async Task<User?> GetUserByIdAsync(string userId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByPhoneAsync(string phone)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
    }

    public async Task<OperationalResult> Register(User user)
    {
        await _context.Users.AddAsync(user);

        if (await SaveChangesAsync())
        {
            return OperationalResult.SuccessResult(new [] {"Registration successful."});
        }
        else
        {
            return OperationalResult.FailureResult(new[]
                { "Failed to add user: no changes detected by the database." });
        }
    }

    public async Task<bool> ValidateRegistrationCredentials(CreateUserDTO credentials)
    {
        // check if the user with the email or phone number already exists
        var doesEmailExist = await _context.Users.AnyAsync(u => u.Email == credentials.Email.Trim());
        var doesPhoneNumberExist = await _context.Users.AnyAsync(u => u.PhoneNumber == credentials.PhoneNumber.Trim());
        if (!doesEmailExist || !doesPhoneNumberExist)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UploadUserImage(ProfilePhoto profilePhoto)
    {
        await _context.ProfilePhoto.AddAsync(profilePhoto);

        return await SaveChangesAsync();
    }
}