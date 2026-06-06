using Contract.Dtos.Trip;

namespace Gateway.Services
{
    public interface ITravelGatewayService
    {
        Task<List<TravelPlanDto>> GetAllTravelPlansAsync(int userId);
        Task<TravelPlanDto?> GetTravelPlanAsync(int id, int userId);
        Task<TravelPlanDto?> GetTravelPlanByIdAsync(int id);
        Task<TravelPlanDto?> CreateTravelPlanAsync(CreateTravelPlanDto dto, int userId);
        Task<bool> UpdateTravelPlanAsync(int id, UpdateTravelPlanDto dto, int userId);
        Task<bool> DeleteTravelPlanAsync(int id, int userId);
        Task<List<DestinationDto>> GetAllDestinationsAsync(int travelPlanId);
        Task<DestinationDto?> GetDestinationAsync(int id);
        Task<DestinationDto?> CreateDestinationAsync(CreateDestinationDto dto, int travelPlanId);
        Task<bool> UpdateDestinationAsync(int id, UpdateDestinationDto dto);
        Task<bool> DeleteDestinationAsync(int id);
        Task<List<ActivityDto>> GetAllActivitiesAsync(int travelId);
        Task<ActivityDto?> GetActivityAsync(int id);
        Task<ActivityDto?> CreateActivityAsync(CreateActivityDto dto, int travelId);
        Task<bool> UpdateActivityAsync(int id, UpdateActivityDto dto);
        Task<bool> DeleteActivityAsync(int id);
        Task<List<CheckListItemResponseDto>> GetAllCheckListItemsAsync(int travelPlanId);
        Task<CheckListItemResponseDto?> CreateCheckListItemAsync(int travelPlanId, CreateChecklistItemDto dto);
        Task<bool> ToggleCheckListItemAsync(int id, bool isCompleted, int travelPlanId);
        Task<bool> DeleteCheckListItemAsync(int id, int travelPlanId);
        Task<ShareResponseDto?> CreateShareAsync(int travelPlanId, CreateShareDto dto);
        Task<SharedTravelPlanDto?> GetShareByTokenAsync(string token);
        Task<SharedTravelPlanDto?> GetPermissionFromTokenAsync(string token);
        Task<List<ActivityDto>> GetActivitiesByDateAsync(int travelPlanId,DateTime date);
        Task<List<TravelPlanDto>> GetAllTravelPlansAdminAsync();
        Task<bool> UpdateTravelPlanAdminAsync(int id, UpdateTravelPlanDto dto);
        Task<bool> DeleteTravelPlanAdminAsync(int id);
    }
}
