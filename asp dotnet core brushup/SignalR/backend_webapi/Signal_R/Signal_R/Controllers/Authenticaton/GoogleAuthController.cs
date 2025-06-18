using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Signal_R.Configuration;
using Signal_R.Models;
using Signal_R.Repository.Interfaces;
using Signal_R.Service.Interfaces;

namespace Signal_R.Controllers.Authenticaton
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoogleAuthController : ControllerBase
    {
        private readonly GoogleSettings _googleSettings;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _userRepo;
        private readonly ITokenService _tokenService;
        private readonly ICookieService _cookieService;

        public GoogleAuthController(
            IOptions<GoogleSettings> googleSettings,
            SignInManager<User> signInManager,
            IUserRepository userRepository,
            ITokenService tokenService,
            ICookieService cookieService)
        {
            _googleSettings = googleSettings.Value;
            _signInManager = signInManager;
            _userRepo = userRepository;
            _cookieService = cookieService;
            _tokenService = tokenService;
        }


        [HttpGet("/api/account/login/google")]
        //[HttpGet("google")]
        public IActionResult SignInWithGoogle()
        {
            //var returnUrl = "http://localhost:5173/user-chats";
            //var callbackUrl = "/api/account/login/google/callback?returnUrl=" + Uri.EscapeDataString(returnUrl);
            var callbackUrl = _googleSettings.CallbackUrl;
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", callbackUrl);

            return Challenge(properties, ["Google"]);
        }


        [HttpGet("/api/account/login/google/callback")]
        public async Task<IActionResult> GoogleSignInCallback([FromQuery] string returnUrl)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return BadRequest("External login info is null");
            }

            var user = await _userRepo.LoginWithGoogle(info.Principal);

            var token = _tokenService.GenerateToken(user);

            var cookieOptions = _cookieService.GetCookieOptions();
            _cookieService.AppendCookies(cookieOptions, HttpContext.Response, token);
            _cookieService.AppendCookies(cookieOptions, HttpContext.Response, user.Id);

            return Redirect(returnUrl);
        }

    }
}
