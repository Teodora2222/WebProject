using Microsoft.AspNetCore.Mvc;
using UserService.Domain.DTOs;
using UserService.Domain.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService loginService;
        private readonly IRegisterService registerService;

        public AuthController(ILoginService loginService, IRegisterService registerService)
        {
            this.loginService = loginService;
            this.registerService = registerService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var token = await loginService.login(dto);
                if (token == null)
                    return Unauthorized(new { success = false, message = "Invalid credentials" });
                return Ok(new { success = true, token = token });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                var result = await registerService.register(dto);
                if (!result)
                    return BadRequest(new { success = false, message = "Email already exists" });

                return Ok(new { success = true, message = "Registration successful" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }
    }
}
