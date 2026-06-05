using Contract.Dtos.User;
using Contract.Services;
using Gateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
        [ApiController]
        [Route("api/auth")]
        public class AuthController : ControllerBase
        {
            private readonly IAuthGatewayService authGatewayService;

            public AuthController(IAuthGatewayService userService)
            {
                   authGatewayService = userService;
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] LoginDto dto)
            {
                try
                {
                    var token = await authGatewayService.LoginAsync(dto);
                    if (token == null)
                        return Unauthorized(new { success = false, message = "Invalid credentials" });
                    return Ok(new { success = true, token = token });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        success = false,
                        message = ex.Message,
                        inner = ex.InnerException?.Message
                    });
                }
            }


            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] RegisterDto dto)
            {
                try
                {
                    var result = await authGatewayService.RegisterAsync(dto);
                    if (!result)
                        return BadRequest(new { success = false, message = "Email already exists" });

                    return Ok(new { success = true, message = "Registration successful" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        success = false,
                        message = ex.Message,
                        inner = ex.InnerException?.Message
                    });
                }
            }
        }
    }

