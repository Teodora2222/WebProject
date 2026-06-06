using Contract.Dtos.Trip;
using Contract.Services;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace Gateway.Services
{

    public class ServiceFabricTravelGatewayService : ITravelGatewayService
    {
        private IValidatorService CreateProxy() =>
            ServiceProxy.Create<IValidatorService>(new Uri("fabric:/WebProject/ValidatorService"));

        public Task<List<TravelPlanDto>> GetAllTravelPlansAsync(int userId) =>
            CreateProxy().GetAllTravelPlans(userId);

        public Task<TravelPlanDto?> GetTravelPlanAsync(int id, int userId) =>
            CreateProxy().GetTravelPlan(id, userId);

        public Task<TravelPlanDto?> GetTravelPlanByIdAsync(int id)
        {
            return CreateProxy().GetTravelPlanById(id);
        }

        public Task<TravelPlanDto?> CreateTravelPlanAsync(CreateTravelPlanDto dto, int userId) =>
            CreateProxy().CreateTravelPlan(dto, userId);

        public Task<bool> UpdateTravelPlanAsync(int id, UpdateTravelPlanDto dto, int userId) =>
            CreateProxy().UpdateTravelPlan(id, dto, userId);

        public Task<bool> DeleteTravelPlanAsync(int id, int userId) =>
            CreateProxy().DeleteTravelPlan(id, userId);

        public Task<List<DestinationDto>> GetAllDestinationsAsync(int travelPlanId) =>
            CreateProxy().GetAllDestinations(travelPlanId);

        public Task<DestinationDto?> GetDestinationAsync(int id) =>
            CreateProxy().GetDestination(id);

        public Task<DestinationDto?> CreateDestinationAsync(CreateDestinationDto dto, int travelPlanId) =>
            CreateProxy().CreateDestination(dto, travelPlanId);

        public Task<bool> UpdateDestinationAsync(int id, UpdateDestinationDto dto) =>
            CreateProxy().UpdateDestination(id, dto);

        public Task<bool> DeleteDestinationAsync(int id) =>
            CreateProxy().DeleteDestination(id);

        public Task<List<ActivityDto>> GetAllActivitiesAsync(int travelId) =>
            CreateProxy().GetAllActivities(travelId);

        public Task<ActivityDto?> GetActivityAsync(int id) =>
            CreateProxy().GetActivity(id);

        public Task<ActivityDto?> CreateActivityAsync(CreateActivityDto dto, int travelId) =>
            CreateProxy().CreateActivity(dto, travelId);

        public Task<bool> UpdateActivityAsync(int id, UpdateActivityDto dto) =>
            CreateProxy().UpdateActivity(id, dto);

        public Task<bool> DeleteActivityAsync(int id) =>
            CreateProxy().DeleteActivity(id);

        public Task<List<CheckListItemResponseDto>> GetAllCheckListItemsAsync(int travelPlanId) =>
            CreateProxy().GetAllCheckListItems(travelPlanId);

        public Task<CheckListItemResponseDto?> CreateCheckListItemAsync(int travelPlanId, CreateChecklistItemDto dto) =>
            CreateProxy().CreateCheckListItem(travelPlanId, dto);

        public Task<bool> ToggleCheckListItemAsync(int id, bool isCompleted, int travelPlanId) =>
            CreateProxy().ToggleCheckListItem(id, isCompleted, travelPlanId);

        public Task<bool> DeleteCheckListItemAsync(int id, int travelPlanId) =>
            CreateProxy().DeleteCheckListItem(id, travelPlanId);

        public Task<ShareResponseDto?> CreateShareAsync(int travelPlanId, CreateShareDto dto) =>
            CreateProxy().CreateShare(travelPlanId, dto);

        public Task<SharedTravelPlanDto?> GetShareByTokenAsync(string token) =>
            CreateProxy().GetShareByToken(token);

        public Task<SharedTravelPlanDto?> GetPermissionFromTokenAsync(string token) => CreateProxy()
                .GetPermissionFromToken(token);

        public Task<List<ActivityDto>> GetActivitiesByDateAsync(int travelPlanId, DateTime date) => CreateProxy()
            .GetActivitiesByDate(travelPlanId, date);

        public Task<List<TravelPlanDto>> GetAllTravelPlansAdminAsync() => CreateProxy() 
            .GetAllTravelPlansAdmin();

        public Task<bool> UpdateTravelPlanAdminAsync(int id, UpdateTravelPlanDto dto) => CreateProxy()
            .UpdateTravelPlanAdmin(id, dto);


        public Task<bool> DeleteTravelPlanAdminAsync(int id) => CreateProxy()
            .DeleteTravelPlanAdmin(id);

    }
}
