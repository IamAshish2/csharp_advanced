using interview_prep.Configuration;
using interview_prep.Models;
using interview_prep.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace interview_prep.Services.Service;

public class CookieService : ICookieService
{
    private readonly JwtSettings _jwtSettings;

    public CookieService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }
    
    
    private CookieOptions GetCookieOptions()
    {
        return new CookieOptions()
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true,
            IsEssential = true,
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_jwtSettings.ExpireMinutes))
        };
    }

    public void AppendCookies(HttpResponse response, string authToken)
    {
        var options = GetCookieOptions();
        response.Cookies.Append("auth_token",authToken, options);
    }
}