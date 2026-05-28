using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripService.Domain.DTOs;
using TripService.Domain.Enum;
using TripService.Domain.Helpers;
using TripService.Domain.Services;
using TripService.Services;

namespace TripService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/travel-plans/{travelPlanId}/checklist")]
    public class CheckListItemController : ControllerBase
    {
        private ICheckListItemService checkListItemService;
        private IShareService shareService;

        public CheckListItemController(ICheckListItemService checkListItemService, IShareService shareService)
        {
            this.checkListItemService = checkListItemService;
            this.shareService = shareService;
        }

        [HttpGet]
        public async Task<IActionResult> getAllItems(int travelPlanId)
        {
            try
            {
                if (!await SharePermissionHelper.HasViewPermission(Request, User, shareService))
                {
                    return Unauthorized();
                }
                var result = await checkListItemService.GetAllCheckListItems(travelPlanId);
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
                if (!await SharePermissionHelper.HasEditPermission(Request, User, shareService))
                {
                    return Forbid("VIEW only");
                }

                var result = await checkListItemService.CreateCheckListItem(travelPlanId,dto);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteItems(int id,int travelPlanId)
        {
            try
            {
                if (!await SharePermissionHelper.HasEditPermission(Request, User, shareService))
                {
                    return Forbid("VIEW only");
                }

                var result = await checkListItemService.DeleteCheckListItem(id, travelPlanId);
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
        public async Task<IActionResult> toggleItems(int id, [FromBody] UpdateChecklistItemDto dto,int travelPlanId)
        {
            try
            {
                if (!await SharePermissionHelper.HasEditPermission(Request, User, shareService))
                {
                    return Forbid("VIEW only");
                }
                var result = await checkListItemService.ToggleCheckListItem(id, dto.IsCompleted,travelPlanId);
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
