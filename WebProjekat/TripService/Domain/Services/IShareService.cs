using TripService.Domain.DTOs;
using TripService.Domain.Enum;

namespace TripService.Domain.Services
{
    public interface IShareService
    {
        Task<ShareResponseDto> CreateShare(int travelPlanId, CreateShareDto dto);

        Task<SharePermission?> GetPermissionFromToken(string token);
    }
}
