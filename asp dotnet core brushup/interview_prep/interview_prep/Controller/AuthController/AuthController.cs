using System.Net;
using interview_prep.Dto.UserDTO;
using interview_prep.Services.Interfaces;
using interview_prep.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace interview_prep.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ICookieService _cookieService;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public AuthController(
        ICookieService cookieService, IUserService userService, ITokenService tokenService)
    {
        _cookieService = cookieService;
        _userService = userService;
        _tokenService = tokenService;
    }


    [HttpPost("login-user")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Login([FromBody] LoginCredentialsDTO credentials)
    {
        // apply custom exceptions
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            var loginResult = await _userService.LoginUserAsync(credentials);
            if (loginResult.Success)
            {
                var result = await _userService.GetUserByEmail(credentials.Email);

                var token = _tokenService.GenerateJwtToken(result.Data);
                // store it in a cookie here
                _cookieService.AppendCookies(Response, token);
                return NoContent();
            }

            if (loginResult.Errors.Count > 0)
            {
                foreach (var error in loginResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return BadRequest(ModelState);
                }
            }
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ProblemDetails()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "Internal Server Occured",
                Detail = "Please try again later. If the problem persists, contact support." + e.Message
            });
        }

        return NoContent();
    }

    [HttpPost("register-user")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> RegisterUser([FromBody] CreateUserDTO credentials)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _userService.RegisterUserAsync(credentials);

            if (result.Success)
            {
                return Created();
            }

            if (result.Errors.Count > 0)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return BadRequest(ModelState);
                }
            }
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ProblemDetails()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "An internal server error occurred.",
                Detail = "Please try again later. If the problem persists, contact support."
            });
        }

        return Created();
    }
}