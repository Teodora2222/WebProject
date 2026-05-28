using System.Fabric.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripService.Data;
using TripService.Domain.DTOs;
using TripService.Domain.Services;

namespace TripService.Controllers
{
    [ApiController]
    [Route("/api/travel-plans/{travelPlanId}/shares")]
    public class ShareController : ControllerBase
    {
        private readonly IShareService shareService;
        private readonly AppDbContext context;

        public ShareController(IShareService shareService, AppDbContext context)
        {
            this.shareService = shareService;
            this.context = context;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SharePlan(int travelPlanId, [FromBody] CreateShareDto dto)
        {
            var result = await shareService.CreateShare(travelPlanId, dto);
            return Ok(result);
        }

        [HttpGet("/api/shared/{token}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSharedPlan(string token)
        {
            var share = await shareService.GetShareByToken(token);

            if (share == null)
                return NotFound(new { message = "Invalid link" });

            var plan = await context.TravelPlans
                .FirstOrDefaultAsync(t => t.id == share.TravelPlanId);

            return Ok(new { plan, permission = share.Permission });
        }

    }
}
