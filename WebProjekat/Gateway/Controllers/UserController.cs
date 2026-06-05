using Contract.Dtos.User;
using Contract.Services;
using Gateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserGatewayService userGatewayService;

        public UserController(IUserGatewayService userService)
        {
            userGatewayService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> getAllUsers()
        {
            try
            {
                var result = await userGatewayService.GetAllUsersAsync();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteUser(int id)
        {
            try
            {
                var result = await userGatewayService.DeleteUserAsync(id);

                if (!result)
                    return NotFound(new { success = false, message = "User not found" });
                return Ok(new { success = true, message = "User deleted" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getUser(int id)
        {
            try
            {
                var result = await userGatewayService.GetUserAsync(id);

                if (result == null)
                    return NotFound(new { success = false, message = "User not found" });

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateUser(int id, [FromBody] UpdateUserDto dto)
        {
            try
            {
                await userGatewayService.UpdateUserAsync(id, dto);
                return Ok(new { success = true, message = "User updated" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

    }
}
