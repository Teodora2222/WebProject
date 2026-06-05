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
    [Route("/api/travel-plans/{travelPlanId}/activities")]
    public class ActivityController : ControllerBase
    {
        private ITravelGatewayService travelGatewayService;
        public ActivityController(ITravelGatewayService travelGatewayService)
        {
            this.travelGatewayService = travelGatewayService;
        }

        [HttpGet]
        public async Task<IActionResult> getAllActivities(int travelPlanId)
        {
            try
            {
                if (!await SharePermissionHelper.HasViewPermission(Request, User, travelGatewayService))
                {
                    return Unauthorized();
                }
                var result = await travelGatewayService.GetAllActivitiesAsync(travelPlanId);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpGet("by-date")]
        public async Task<IActionResult> getActivitiesByDate(int travelPlanId, [FromQuery] DateTime date)
        {
            try
            {
                if (!await SharePermissionHelper.HasViewPermission(Request, User, travelGatewayService))
                {
                    return Unauthorized();
                }
                var result = await travelGatewayService.GetActivitiesByDateAsync(travelPlanId, date);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> createActivity(int travelPlanId, [FromBody] CreateActivityDto dto)
        {
            try
            {
                if (!await SharePermissionHelper.HasEditPermission(Request, User, travelGatewayService))
                {
                    return Forbid("VIEW only");
                }

                var result = await travelGatewayService.CreateActivityAsync(dto, travelPlanId);
                return CreatedAtAction(nameof(getActivity), new { travelPlanId, id = result.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteActivity(int id)
        {
            try
            {
                if (!await SharePermissionHelper.HasEditPermission(Request, User, travelGatewayService))
                {
                    return Forbid("VIEW only");
                }

                var result = await travelGatewayService.DeleteActivityAsync(id);
                if (!result)
                    return NotFound(new { success = false, message = "Activity not found" });

                return Ok(new { success = true, message = "Activity  deleted" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateActivity(int id, [FromBody] UpdateActivityDto dto)
        {
            try
            {
                if (!await SharePermissionHelper.HasEditPermission(Request, User, travelGatewayService))
                {
                    return Forbid("VIEW only");
                }

                var result = await travelGatewayService.UpdateActivityAsync(id, dto);
                if (!result)
                    return NotFound(new { success = false, message = "Activity not found" });
                return Ok(new { success = true, message = "Activity updated" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getActivity(int id)
        {
            try
            {
                if (!await SharePermissionHelper.HasViewPermission(Request, User, travelGatewayService))
                {
                    return Unauthorized();
                }

                var result = await travelGatewayService.GetActivityAsync(id);
                if (result == null)
                    return NotFound(new { success = false, message = "Activity not found" });

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }
    }
}
