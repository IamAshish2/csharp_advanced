using Microsoft.Extensions.Options;
using Signal_R.Configuration;
using Signal_R.Service.Interfaces;

namespace Signal_R.Service.Services
{
    public class CookieService : ICookieService
    {
        private readonly JwtSettings _jwtSettings;
        public CookieService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public void AppendCookies(CookieOptions options, HttpResponse response, string token)
        {
            response.Cookies.Append("authToken",token,options);
        }

        public CookieOptions GetCookieOptions ()
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
    }
}
