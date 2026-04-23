using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripService.Domain.DTOs;
using TripService.Domain.Services;

namespace TripService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/travel")]
    public class TravelPlanController : ControllerBase
    {
        private ITravelPlanService travelPlanService;

        public TravelPlanController(ITravelPlanService travelPlanService)
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
                var userId = GetUserId();
                var result = await travelPlanService.getAllTravelPlans(userId);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getTravelPlan(int id)
        {
            try
            {
                var userId = GetUserId();
                var result = await travelPlanService.getTravelPlan(id,userId);
                if (result == null)
                    return NotFound(new { success = false, message = "User not found" });

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteTravelPlan(int id)
        {
            try
            {
                var userId = GetUserId();
                var result = await travelPlanService.deleteTravelPlan(id,userId);
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
                var userId = GetUserId();
                var result = await travelPlanService.updateTravelPlan(id,dto,userId);
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
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null)
                    return Unauthorized(new { success = false, message = "Invalid token" });

                int userId = int.Parse(userIdClaim);
                var result = await travelPlanService.createTravelPlan(dto,userId);
                return Ok(new { success = true, message = "Travel plan created" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }
    }
}
