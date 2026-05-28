using Microsoft.EntityFrameworkCore;
using TripService.Data;
using TripService.Domain.DTOs;
using TripService.Domain.Enum;
using TripService.Domain.Models;
using TripService.Domain.Services;

namespace TripService.Services
{
    public class ShareService : IShareService
    {
        private readonly AppDbContext context;

        public ShareService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<ShareResponseDto> CreateShare(int travelPlanId, CreateShareDto dto)
        {
            var share = new SharedTravelPlan
            {
                TravelPlanId = travelPlanId,
                Permission = dto.Permission,
                Token = Guid.NewGuid().ToString()
            };

            context.SharedTravelPlans.Add(share);
            await context.SaveChangesAsync();

            //var url = $"http://192.168.1.5:5173/shared/{share.Token}";
            var url = $"http://localhost:5173/shared/{share.Token}";

            return new ShareResponseDto
            {
                Url = url,
                Token = share.Token
            };
        }

        public async Task<SharedTravelPlan?> GetShareByToken(string token)
        {
            return await context.SharedTravelPlans
                .FirstOrDefaultAsync(s => s.Token == token);
        }

        public async Task<SharePermission?> GetPermissionFromToken(string token)
        {
            var share = await context.SharedTravelPlans
                .FirstOrDefaultAsync(s => s.Token == token);

            return share != null ? Enum.Parse<SharePermission>(share.Permission) : null;
        }
    }
}
