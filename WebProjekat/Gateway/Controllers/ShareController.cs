using Contract.Dtos.Trip;
using Contract.Services;
using Gateway.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("/api/travel-plans/{travelPlanId}/shares")]
    public class ShareController : ControllerBase
    {
        private readonly ITravelGatewayService shareService;

        public ShareController(ITravelGatewayService shareService)
        {
            this.shareService = shareService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SharePlan(int travelPlanId, [FromBody] CreateShareDto dto)
        {
            var result = await shareService.CreateShareAsync(travelPlanId, dto);
            return Ok(result);
        }

        [HttpGet("/api/shared/{token}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSharedPlan(string token)
        {
            var share = await shareService.GetPermissionFromTokenAsync(token);

            if (share == null)
                return NotFound(new { message = "Invalid link" });

            var plan = await shareService.GetTravelPlanByIdAsync(
                    share.TravelPlanId);

            if (plan == null)
                return NotFound(new { message = "Plan not found" });

            return Ok(new
            {
                plan,
                permission = share.Permission
            });
        }

    }
}
