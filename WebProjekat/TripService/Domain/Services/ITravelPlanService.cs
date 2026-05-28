using TripService.Domain.DTOs;
using TripService.Domain.Models;

namespace TripService.Domain.Services
{
    public interface ITravelPlanService
    {
        public Task<List<TravelPlan>> getAllTravelPlans(int userId);

        public Task<TravelPlan> getTravelPlan(int id, int userId);

        public Task<bool> deleteTravelPlan(int id, int userId);

        public Task<TravelPlan> createTravelPlan(CreateTravelPlanDto dto, int userId);

        public Task<bool> updateTravelPlan(int id,UpdateTravelPlanDto dto, int userId);

        Task<List<TravelPlan>> getAllTravelPlansAdmin();

    }
}
