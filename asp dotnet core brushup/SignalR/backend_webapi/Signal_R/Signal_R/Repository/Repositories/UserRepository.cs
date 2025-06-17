using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Signal_R.Data;
using Signal_R.global.Exceptions;
using Signal_R.Models;
using Signal_R.Repository.Interfaces;
using System.Security.Claims;

namespace Signal_R.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
         _context = context;   
        }
        public async Task<IdentityUser> LoginWithGoogle(ClaimsPrincipal? claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                throw new ExternalLoginProviderException("Google", "Claims principal is null");
            }

            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email) ?? throw new ExternalLoginProviderException("Google", "Email is null");

            // if the claimsPrincipal does not have email attached
            if (email == null)
            {
                throw new ExternalLoginProviderException("Google", "Email is null");
            }

            // check if we already have a user with the email, 
            // meaning the user is already a member of our platform
            var user = await GetUserByEmailAsync(email);

            // create a new user now
            if (user == null)
            {
                var newUser = new User
                {
                    //FullName = claimsPrincipal.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty,
                    UserName = claimsPrincipal.FindFirstValue(ClaimTypes.Name) ?? string.Empty,
                    Email = email,
                };

                var result = await CreateUserAsync(newUser);

                if (!result)
                {
                    throw new ExternalLoginProviderException("Google", "The user could not be created at the moment.");
                }

                var getUser = await GetUserByEmailAsync(newUser.Email);
                return getUser;
                // var mapUser = _mapper.Map<User>(getUser);
                // var jwt = _tokenGenerator.GenerateToken(mapUser);
                // return jwt;
            }

            // for user already exists
            // var userName = claimsPrincipal.FindFirstValue(ClaimTypes.Name);
            //var mappedUser = _mapper.Map<User>(user);
            //return mappedUser;
            // var jwtToken = _tokenGenerator.GenerateToken(mappedUser);
            // return jwtToken;
            return user;
        }

        public async Task<bool> CreateUserAsync(User newUser)
        {
            await _context.Users.AddAsync(newUser);
            return await SaveAsync();
        }

        public async Task<bool> DeleteUserAsync(string UserName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == UserName);
            if (user != null)
            {

                _context.Users.Remove(user);
            }
            return await SaveAsync();
        }

        public async Task<bool> ChangePasswordAsync(string email, string password)
        {
            // get the user
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                user.PasswordHash = password;
            }

            return await SaveAsync();
        }

        public async Task<IdentityUser> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null;
            }

            return user;

        }

        public async Task<ICollection<IdentityUser>> GetUsersAsync()
        {
            return await _context.Users.OrderBy(u => u.UserName).ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            return await SaveAsync();
        }

        public async Task<bool> isEmailInUse(string Email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);
            return user == null ? false : true;
        }

        public async Task<bool> isUserNameInUse(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            return user == null ? false : true;
        }
        public async Task<bool> checkIfUserExists(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user == null ? false : true;
        }

        public async Task<IdentityUser> GetUserByNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
