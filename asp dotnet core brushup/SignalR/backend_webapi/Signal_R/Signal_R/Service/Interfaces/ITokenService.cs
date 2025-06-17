using Microsoft.AspNetCore.Identity;

namespace Signal_R.Service.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(IdentityUser user);
        //string GenerateRefreshToken();
    }
}
