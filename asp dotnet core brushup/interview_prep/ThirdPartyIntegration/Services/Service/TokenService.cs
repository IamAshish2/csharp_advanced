using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using interview_prep.Models;
using interview_prep.Models.user;
using interview_prep.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace interview_prep.Services.Service;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateJwtToken(User user)
    {
        // security Key
        // signing credentials
        //  claims
        // JwtSecurityToken
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new []
        {
            new Claim(ClaimTypes.Sid, user.Id)
        };

        var jwtSecurityToken = new JwtSecurityToken(
            _config["JWT:Issuer"], 
            _config["JWT:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(Convert.ToInt32(_config["JWT:ExpireMinutes"])),
            signingCredentials:credentials
        );

        return  new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}