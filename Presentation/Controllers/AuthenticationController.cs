using Application.Services.Authentication;
using Contract.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public AuthenticationResult Register(RegisterRequest request)
        {
            return _authenticationService.Register(request.FirstName,
                    request.LastName,
                    request.Email,
                    request.Password
                );
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            return Ok();
        }
    }
}
