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
    [Route("/api/travel-plans/{travelPlanId}/checklist")]
    public class CheckListItemController : ControllerBase
    {
        private ITravelGatewayService travelGatewayService;

        public CheckListItemController(ITravelGatewayService travelGatewayService)
        {
            this.travelGatewayService = travelGatewayService;
        }

        [HttpGet]
        public async Task<IActionResult> getAllItems(int travelPlanId)
        {
            try
            {
                if (!await SharePermissionHelper.HasViewPermission(Request, User, travelGatewayService))
                {
                    return Unauthorized();
                }
                var result = await travelGatewayService.GetAllCheckListItemsAsync(travelPlanId);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> createItems(int travelPlanId, [FromBody] CreateChecklistItemDto dto)
        {
            try
            {
                if (!await SharePermissionHelper.HasEditPermission(Request, User, travelGatewayService))
                {
                    return Forbid("VIEW only");
                }

                var result = await travelGatewayService.CreateCheckListItemAsync(travelPlanId, dto);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteItems(int id, int travelPlanId)
        {
            try
            {
                if (!await SharePermissionHelper.HasEditPermission(Request, User, travelGatewayService))
                {
                    return Forbid("VIEW only");
                }

                var result = await travelGatewayService.DeleteCheckListItemAsync(id, travelPlanId);
                if (!result)
                    return NotFound(new { success = false, message = "Check List Item not found" });

                return Ok(new { success = true, message = "Check List Item  deleted" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> toggleItems(int id, [FromBody] UpdateChecklistItemDto dto, int travelPlanId)
        {
            try
            {
                if (!await SharePermissionHelper.HasEditPermission(Request, User, travelGatewayService))
                {
                    return Forbid("VIEW only");
                }
                var result = await travelGatewayService.ToggleCheckListItemAsync(id, dto.IsCompleted, travelPlanId);
                if (!result)
                    return NotFound(new { success = false, message = "Check List Item not found" });
                return Ok(new { success = true, message = "Check List Item updated" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }
    }
}
