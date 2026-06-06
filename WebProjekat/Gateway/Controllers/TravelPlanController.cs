using System.Data;
using System.Security.Claims;
using Contract.Dtos.Trip;
using Contract.Services;
using Gateway.Helpers;
using Gateway.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/travel-plans")]
    public class TravelPlanController : ControllerBase
    {
        private ITravelGatewayService travelPlanService;

        public TravelPlanController(ITravelGatewayService travelPlanService)
        {
            this.travelPlanService = travelPlanService;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        [HttpGet]
        public async Task<IActionResult> getAllTravelPlans()
        {
            try
            {
                if (!await SharePermissionHelper.HasViewPermission(Request, User, travelPlanService))
                {
                    return Unauthorized();
                }
                var role = User.FindFirst(ClaimTypes.Role)?.Value;

                if (role == "ADMIN")
                {
                    var allPlans = await travelPlanService.GetAllTravelPlansAdminAsync();
                    return Ok(allPlans);
                }

                var userId = GetUserId();
                var result = await travelPlanService.GetAllTravelPlansAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message,
                    inner = ex.InnerException?.Message,
                    stack = ex.StackTrace
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getTravelPlan(int id)
        {
            try
            {
                if (!await SharePermissionHelper.HasViewPermission(
                    Request, User, travelPlanService))
                {
                    return Unauthorized();
                }

                var role = User.FindFirst(ClaimTypes.Role)?.Value;

                if (role == "ADMIN")
                {
                    var adminResult =
                        await travelPlanService.GetTravelPlanByIdAsync(id);

                    if (adminResult == null)
                        return NotFound();

                    return Ok(adminResult);
                }

                var userId = GetUserId();

                var result =
                    await travelPlanService.GetTravelPlanAsync(id, userId);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteTravelPlan(int id)
        {
            try
            {
                if (!await SharePermissionHelper.HasEditPermission(Request, User, travelPlanService))
                {
                    return Forbid("VIEW only");
                }

                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                var userId = GetUserId();
                bool result;

                if (role == "ADMIN")
                    result = await travelPlanService.DeleteTravelPlanAdminAsync(id);
                else
                    result = await travelPlanService.DeleteTravelPlanAsync(id, userId);

                if (!result)
                    return NotFound(new { success = false, message = "Travel Plan not found" });

                return Ok(new { success = true, message = "Travel Plan deleted" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> updateTravelPlan(int id, [FromBody] UpdateTravelPlanDto dto)
        {
            try
            {
                if (!await SharePermissionHelper.HasEditPermission(Request, User, travelPlanService))
                {
                    return Forbid("VIEW only");
                }

                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                var userId = GetUserId();

                bool result;
                if (role == "ADMIN")
                    result = await travelPlanService.UpdateTravelPlanAdminAsync(id, dto);
                else
                    result = await travelPlanService.UpdateTravelPlanAsync(id, dto, userId);

                if (!result)
                    return NotFound(new { success = false, message = "Not found" });

                return Ok(new { success = true, message = "Travel plan updated" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> createTravelPlan([FromBody] CreateTravelPlanDto dto)
        {
            try
            {
                if (!await SharePermissionHelper.HasEditPermission(Request, User, travelPlanService))
                {
                    return Forbid("VIEW only");
                }

                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null)
                    return Unauthorized(new { success = false, message = "Invalid token" });

                int userId = int.Parse(userIdClaim);
                var result = await travelPlanService.CreateTravelPlanAsync(dto, userId);
                return Ok(new { success = true, message = "Travel plan created" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }
    }
}
