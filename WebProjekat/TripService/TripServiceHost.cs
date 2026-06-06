using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using Contract.Dtos.Trip;
using Contract.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace TripService
{
    internal sealed class TripServiceHost : StatelessService,
        ITravelPlanService,
        IDestinationService,
        IActivityService,
        ICheckListItemService,
        IShareService
    {
        private readonly IServiceProvider serviceProvider;

        public TripServiceHost(StatelessServiceContext context, IServiceProvider serviceProvider)
            : base(context)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task<List<TravelPlanDto>> getAllTravelPlans(int userId) =>
            ExecuteAsync<ITravelPlanService, List<TravelPlanDto>>(s => s.getAllTravelPlans(userId));

        public Task<TravelPlanDto> getTravelPlan(int id, int userId) =>
            ExecuteAsync<ITravelPlanService, TravelPlanDto>(s => s.getTravelPlan(id, userId));

        public Task<bool> deleteTravelPlan(int id, int userId) =>
            ExecuteAsync<ITravelPlanService, bool>(s => s.deleteTravelPlan(id, userId));

        public Task<TravelPlanDto> createTravelPlan(CreateTravelPlanDto dto, int userId) =>
            ExecuteAsync<ITravelPlanService, TravelPlanDto>(s => s.createTravelPlan(dto, userId));

        public Task<bool> updateTravelPlan(int id, UpdateTravelPlanDto dto, int userId) =>
            ExecuteAsync<ITravelPlanService, bool>(s => s.updateTravelPlan(id, dto, userId));

        public Task<TravelPlanDto?> getTravelPlanById(int id) => 
            ExecuteAsync<ITravelPlanService, TravelPlanDto?>(s => s.getTravelPlanById(id));

        public Task<bool> updateTravelPlanAdmin(int id, UpdateTravelPlanDto dto) =>
            ExecuteAsync<ITravelPlanService, bool>(s => s.updateTravelPlanAdmin(id, dto));


        public Task<bool> deleteTravelPlanAdmin(int id) =>
            ExecuteAsync<ITravelPlanService, bool>(s => s.deleteTravelPlanAdmin(id));


        public Task<List<TravelPlanDto>> getAllTravelPlansAdmin() =>
            ExecuteAsync<ITravelPlanService, List<TravelPlanDto>>(s => s.getAllTravelPlansAdmin());

        public Task<List<DestinationDto>> getAllDestinastons(int travelId) =>
            ExecuteAsync<IDestinationService, List<DestinationDto>>(s => s.getAllDestinastons(travelId));

        public Task<bool> deleteDestination(int id) =>
            ExecuteAsync<IDestinationService, bool>(s => s.deleteDestination(id));

        public Task<bool> updateDestination(int id, UpdateDestinationDto dto) =>
            ExecuteAsync<IDestinationService, bool>(s => s.updateDestination(id, dto));

        public Task<DestinationDto> getDestination(int id) =>
            ExecuteAsync<IDestinationService, DestinationDto>(s => s.getDestination(id));

        public Task<DestinationDto> createDestination(CreateDestinationDto dto, int travelPlanId) =>
            ExecuteAsync<IDestinationService, DestinationDto>(s => s.createDestination(dto, travelPlanId));

        public Task<List<ActivityDto>> getAllActivities(int travelId) =>
            ExecuteAsync<IActivityService, List<ActivityDto>>(s => s.getAllActivities(travelId));

        public Task<ActivityDto> getActivity(int id) =>
            ExecuteAsync<IActivityService, ActivityDto>(s => s.getActivity(id));

        public Task<List<ActivityDto>> getActivitiesByDate(int travelPlanId, DateTime date) =>
            ExecuteAsync<IActivityService, List<ActivityDto>>(s => s.getActivitiesByDate(travelPlanId, date));

        public Task<bool> deleteActivity(int id) =>
            ExecuteAsync<IActivityService, bool>(s => s.deleteActivity(id));

        public Task<bool> updateActivity(int id, UpdateActivityDto dto) =>
            ExecuteAsync<IActivityService, bool>(s => s.updateActivity(id, dto));

        public Task<ActivityDto> createActivity(CreateActivityDto dto, int travelId) =>
            ExecuteAsync<IActivityService, ActivityDto>(s => s.createActivity(dto, travelId));

        public Task<CheckListItemResponseDto> CreateCheckListItem(int travelPlanId, CreateChecklistItemDto dto) =>
            ExecuteAsync<ICheckListItemService, CheckListItemResponseDto>(s => s.CreateCheckListItem(travelPlanId, dto));

        public Task<List<CheckListItemResponseDto>> GetAllCheckListItems(int travelPlanId) =>
            ExecuteAsync<ICheckListItemService, List<CheckListItemResponseDto>>(s => s.GetAllCheckListItems(travelPlanId));

        public Task<bool> ToggleCheckListItem(int id, bool isCompleted, int travelPlanId) =>
            ExecuteAsync<ICheckListItemService, bool>(s => s.ToggleCheckListItem(id, isCompleted, travelPlanId));

        public Task<bool> DeleteCheckListItem(int id, int travelPlanId) =>
            ExecuteAsync<ICheckListItemService, bool>(s => s.DeleteCheckListItem(id, travelPlanId));

        public Task<ShareResponseDto> CreateShare(int travelPlanId, CreateShareDto dto) =>
            ExecuteAsync<IShareService, ShareResponseDto>(s => s.CreateShare(travelPlanId, dto));

        public Task<SharedTravelPlanDto?> GetPermissionFromToken(string token) =>
            ExecuteAsync<IShareService, SharedTravelPlanDto?>(s => s.GetPermissionFromToken(token));

        public Task<SharedTravelPlanDto?> GetShareByToken(string token) =>
            ExecuteAsync<IShareService, SharedTravelPlanDto?>(s => s.GetShareByToken(token));

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return this.CreateServiceRemotingInstanceListeners();
        }

        private async Task<TResult> ExecuteAsync<TService, TResult>(Func<TService, Task<TResult>> action)
        {
            using var scope = serviceProvider.CreateScope();
            var svc = scope.ServiceProvider.GetRequiredService<TService>();
            return await action(svc);
        }

    }
}