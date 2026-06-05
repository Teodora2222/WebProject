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
    [Route("/api/travel-plans/{travelPlanId}/destinations")]
    public class DestinationController : ControllerBase
    {
        private ITravelGatewayService destinationService;

        public DestinationController(ITravelGatewayService destinationService)
        {
            this.destinationService = destinationService;
        }


        [HttpGet]
        public async Task<IActionResult> getAllDestinations(int travelPlanId)
        {
            try
            {
                if (!await SharePermissionHelper.HasViewPermission(Request, User, destinationService))
                {
                    return Unauthorized();
                }
                var result = await destinationService.GetAllDestinationsAsync(travelPlanId);
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
                if (!await SharePermissionHelper.HasEditPermission(Request, User, destinationService))
                {
                    return Forbid("VIEW only");
                }
                var result = await destinationService.CreateDestinationAsync(dto, travelPlanId);
                return CreatedAtAction(nameof(getDestination), new { travelPlanId, id = result.Id }, result);
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
                if (!await SharePermissionHelper.HasEditPermission(Request, User, destinationService))
                {
                    return Forbid("VIEW only");
                }
                var result = await destinationService.DeleteDestinationAsync(id);
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
                if (!await SharePermissionHelper.HasEditPermission(Request, User, destinationService))
                {
                    return Forbid("VIEW only");
                }
                var result = await destinationService.UpdateDestinationAsync(id, dto);
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
                if (!await SharePermissionHelper.HasViewPermission(Request, User, destinationService))
                {
                    return Unauthorized();
                }
                var result = await destinationService.GetDestinationAsync(id);
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
