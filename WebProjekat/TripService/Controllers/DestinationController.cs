using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using TripService.Domain.DTOs;
using TripService.Domain.Enum;
using TripService.Domain.Helpers;
using TripService.Domain.Services;
using TripService.Services;

namespace TripService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/travel-plans/{travelPlanId}/destinations")]
    public class DestinationController : ControllerBase
    {
        private IDestinationService destinationService;
        private IShareService shareService;

        public DestinationController(IDestinationService destinationService, IShareService shareService)
        {
            this.destinationService = destinationService;
            this.shareService = shareService;
        }


        [HttpGet]
        public async Task<IActionResult> getAllDestinations(int travelPlanId)
        {
            try
            {
                if (!await SharePermissionHelper.HasViewPermission(Request, User, shareService))
                {
                    return Unauthorized();
                }
                var result = await destinationService.getAllDestinastons(travelPlanId);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> createDestination(int travelPlanId, [FromBody] CreateDestinationDto dto)
        {
            try
            {
                if (!await SharePermissionHelper.HasEditPermission(Request, User, shareService))
                {
                    return Forbid("VIEW only");
                }
                var result = await destinationService.createDestination(dto, travelPlanId);
                return CreatedAtAction(nameof(getDestination), new { travelPlanId, id = result.id }, result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteDestination(int id)
        {
            try
            {
                if (!await SharePermissionHelper.HasEditPermission(Request, User, shareService))
                {
                    return Forbid("VIEW only");
                }
                var result = await destinationService.deleteDestination(id);
                if (!result)
                    return NotFound(new { success = false, message = "Destination not found" });

                return Ok(new { success = true, message = "Destination  deleted" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateDestination(int id, [FromBody] UpdateDestinationDto dto)
        {
            try
            {
                if (!await SharePermissionHelper.HasEditPermission(Request, User, shareService))
                {
                    return Forbid("VIEW only");
                }
                var result = await destinationService.updateDestination(id, dto);
                if (!result)
                    return NotFound(new { success = false, message = "Destination not found" });
                return Ok(new { success = true, message = "Destination updated" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getDestination(int id)
        {
            try
            {
                if (!await SharePermissionHelper.HasViewPermission(Request, User, shareService))
                {
                    return Unauthorized();
                }
                var result = await destinationService.getDestination(id);
                if (result == null)
                    return NotFound(new { success = false, message = "Destination not found" });

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }
    }
}
