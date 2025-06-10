using interview_prep.Models;
using interview_prep.Models.user;

namespace interview_prep.Services.Interfaces;

public interface ITokenService
{
    string GenerateJwtToken(User user);
}