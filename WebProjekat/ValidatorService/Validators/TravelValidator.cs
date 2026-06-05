using Contract.Dtos.Trip;
using ValidatorService.Clients;

namespace ValidatorService.Validators
{
    public class TravelValidator
    {
        private readonly TravelPlanServiceClient travelClient;
        private readonly DestinationServiceClient destinationClient;
        private readonly ActivityServiceClient activityClient;
        private readonly ChecklistServiceClient checkListClient;
        private readonly ShareServiceClient shareClient;

        public TravelValidator(
            TravelPlanServiceClient travelClient,
            DestinationServiceClient destinationClient,
            ActivityServiceClient activityClient,
            ChecklistServiceClient checkListClient,
            ShareServiceClient shareClient)
        {
            this.travelClient = travelClient;
            this.destinationClient = destinationClient;
            this.activityClient = activityClient;
            this.checkListClient = checkListClient;
            this.shareClient = shareClient;
        }

        public async Task<TravelPlanDto?> CreateTravelPlan(CreateTravelPlanDto dto, int userId)
        {
            if (dto.EndDate < dto.StartDate)
                return null;

            if (dto.Budget < 0)
                return null;

            return await travelClient.CreateProxy()
                .createTravelPlan(dto, userId);
        }

        public async Task<bool> UpdateTravelPlan(int id, UpdateTravelPlanDto dto, int userId)
        {
            if (id <= 0)
                return false;

            return await travelClient.CreateProxy()
                .updateTravelPlan(id, dto, userId);
        }

        public async Task<bool> DeleteTravelPlan(int id, int userId)
        {
            if (id <= 0)
                return false;

            return await travelClient.CreateProxy()
                .deleteTravelPlan(id, userId);
        }

        public async Task<TravelPlanDto?> GetTravelPlan(int id, int userId)
        {
            if (id <= 0)
                return null;

            return await travelClient.CreateProxy()
                .getTravelPlan(id, userId);
        }

        public async Task<List<TravelPlanDto>> GetAllTravelPlans(int userId)
        {
            return await travelClient.CreateProxy()
                .getAllTravelPlans(userId);
        }

        public async Task<List<TravelPlanDto>> GetAllTravelPlansAdmin()
        {
            return await travelClient.CreateProxy()
                .getAllTravelPlansAdmin();
        }


        public async Task<DestinationDto?> CreateDestination(
            CreateDestinationDto dto,
            int travelPlanId)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return null;

            return await destinationClient.CreateProxy()
                .createDestination(dto, travelPlanId);
        }

        public async Task<bool> UpdateDestination(
            int id,
            UpdateDestinationDto dto)
        {
            if (id <= 0)
                return false;

            return await destinationClient.CreateProxy()
                .updateDestination(id, dto);
        }

        public async Task<bool> DeleteDestination(int id)
        {
            if (id <= 0)
                return false;

            return await destinationClient.CreateProxy()
                .deleteDestination(id);
        }

        public async Task<DestinationDto?> GetDestination(int id)
        {
            if (id <= 0)
                return null;

            return await destinationClient.CreateProxy()
                .getDestination(id);
        }

        public async Task<List<DestinationDto>> GetAllDestinations(int travelPlanId)
        {
            return await destinationClient.CreateProxy()
                .getAllDestinastons(travelPlanId);
        }

        public async Task<ActivityDto?> CreateActivity(
            CreateActivityDto dto,
            int travelId)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return null;

            return await activityClient.CreateProxy()
                .createActivity(dto, travelId);
        }

        public async Task<bool> UpdateActivity(
            int id,
            UpdateActivityDto dto)
        {
            if (id <= 0)
                return false;

            return await activityClient.CreateProxy()
                .updateActivity(id, dto);
        }

        public async Task<List<ActivityDto>> GetActivitiesByDate(
            int travelPlanId,
            DateTime date)
        {
            return await activityClient.CreateProxy()
                .getActivitiesByDate(travelPlanId, date);
        }

        public async Task<bool> DeleteActivity(int id)
        {
            if (id <= 0)
                return false;

            return await activityClient.CreateProxy()
                .deleteActivity(id);
        }

        public async Task<ActivityDto?> GetActivity(int id)
        {
            if (id <= 0)
                return null;

            return await activityClient.CreateProxy()
                .getActivity(id);
        }

        public async Task<List<ActivityDto>> GetAllActivities(int travelId)
        {
            return await activityClient.CreateProxy()
                .getAllActivities(travelId);
        }

        public async Task<CheckListItemResponseDto?> CreateCheckListItem(
            int travelPlanId,
            CreateChecklistItemDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return null;

            return await checkListClient.CreateProxy()
                .CreateCheckListItem(travelPlanId, dto);
        }

        public async Task<bool> DeleteCheckListItem(
            int id,
            int travelPlanId)
        {
            if (id <= 0)
                return false;

            return await checkListClient.CreateProxy()
                .DeleteCheckListItem(id, travelPlanId);
        }

        public async Task<bool> ToggleCheckListItem(
            int id,
            bool isCompleted,
            int travelPlanId)
        {
            if (id <= 0)
                return false;

            return await checkListClient.CreateProxy()
                .ToggleCheckListItem(id, isCompleted, travelPlanId);
        }

        public async Task<List<CheckListItemResponseDto>> GetAllCheckListItems(
            int travelPlanId)
        {
            return await checkListClient.CreateProxy()
                .GetAllCheckListItems(travelPlanId);
        }

        public async Task<ShareResponseDto?> CreateShare(
            int travelPlanId,
            CreateShareDto dto)
        {
            if (travelPlanId <= 0)
                return null;

            return await shareClient.CreateProxy()
                .CreateShare(travelPlanId, dto);
        }

        public async Task<SharedTravelPlanDto?> GetShareByToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            return await shareClient.CreateProxy()
                .GetShareByToken(token);
        }

        public async Task<SharedTravelPlanDto?> GetPermissionFromToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            return await shareClient.CreateProxy()
                .GetPermissionFromToken(token);
        }

        public async Task<TravelPlanDto?> GetTravelPlanById(int id)
        {
            if (id <= 0)
                return null;

            return await travelClient
                .CreateProxy()
                .getTravelPlanById(id);
        }
    }
}