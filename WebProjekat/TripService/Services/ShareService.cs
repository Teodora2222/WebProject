using Contract.Dtos.Trip;
using Contract.Services;
using Microsoft.EntityFrameworkCore;
using TripService.Data;
using TripService.Domain.Models;

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

        public async Task<SharedTravelPlanDto?> GetShareByToken(string token)
        {
            var share = await context.SharedTravelPlans
                .FirstOrDefaultAsync(s => s.Token == token);

            if (share == null)
                return null;

            return new SharedTravelPlanDto
            {
                Id = share.Id,
                TravelPlanId = share.TravelPlanId,
                Token = share.Token,
                Permission = share.Permission,
                CreatedAt = share.CreatedAt,
                ExpiresAt = share.ExpiresAt
            };
        }

        public async Task<SharedTravelPlanDto?> GetPermissionFromToken(string token)
        {
            var share = await context.SharedTravelPlans
                .FirstOrDefaultAsync(s => s.Token == token);

            if (share == null)
                return null;

            return new SharedTravelPlanDto
            {
                Id = share.Id,
                TravelPlanId = share.TravelPlanId,
                Token = share.Token,
                Permission = share.Permission,
                CreatedAt = share.CreatedAt,
                ExpiresAt = share.ExpiresAt
            };
        }
    }
}
