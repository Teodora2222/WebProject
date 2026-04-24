using Microsoft.AspNetCore.Mvc;
using UserService.Domain.DTOs;
using UserService.Domain.Models;
using UserService.Domain.Services;
using UserService.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> getAllUsers()
        {
            try
            {
                var result =await userService.getAllUsers();
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
                var result =await userService.deleteUser(id);

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
        public  async Task<IActionResult> getUser(int id)
        {
            try
            {
                var result = await userService.getUser(id);

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
                await userService.updateUser(id,dto);
                return Ok(new { success = true, message = "User updated" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

    }
}
