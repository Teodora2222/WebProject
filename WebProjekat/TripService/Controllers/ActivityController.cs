using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripService.Domain.DTOs;
using TripService.Domain.Services;
using TripService.Services;

namespace TripService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/travel-plans/{travelPlanId}/activities")]
    public class ActivityController : ControllerBase
    {
        private IActivityService activityService;

        public ActivityController(IActivityService activityService)
        {
            this.activityService = activityService;
        }

        [HttpGet]
        public async Task<IActionResult> getAllActivities(int travelPlanId)
        {
            try
            {
                var result = await activityService.getAllActivities(travelPlanId);
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
                var result = await activityService.getActivitiesByDate(travelPlanId, date);
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
                var result = await activityService.createActivity(dto, travelPlanId);
                return CreatedAtAction(nameof(getActivity), new { travelPlanId, id = result.id }, result);
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
                var result = await activityService.deleteActivity(id);
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
                var result = await activityService.updateActivity(id, dto);
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
                var result = await activityService.getActivity(id);
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
