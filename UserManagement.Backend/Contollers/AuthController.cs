using Microsoft.AspNetCore.Mvc;
using UserManagement.Backend.JWTToken;

namespace UserManagement.Backend.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;

        public AuthController(JwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest.Username == "test" && loginRequest.Password == "password") // Simple validation, replace with real authentication logic
            {
                var token = _jwtTokenService.GenerateToken(loginRequest.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid credentials");
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
