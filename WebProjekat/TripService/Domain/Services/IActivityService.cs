using TripService.Domain.DTOs;
using TripService.Domain.Models;

namespace TripService.Domain.Services
{
    public interface IActivityService
    {
        public Task<List<Activity>> getAllActivities(int travelId);

        public Task<Activity> getActivity(int id);

        public Task<List<Activity>> getActivitiesByDate(int travelPlanId, DateTime date);

        public Task<bool> deleteActivity(int id);

        public Task<bool> updateActivity(int id, UpdateActivityDto dto);

        public Task<Activity> createActivity(CreateActivityDto dto,int travelId);
    }
}
