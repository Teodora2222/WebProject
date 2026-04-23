using Microsoft.EntityFrameworkCore;
using TripService.Data;
using TripService.Domain.DTOs;
using TripService.Domain.Models;
using TripService.Domain.Services;

namespace TripService.Services
{
    public class TravelPlanService : ITravelPlanService
    {
        private readonly AppDbContext context;

        public TravelPlanService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<TravelPlan> createTravelPlan(CreateTravelPlanDto dto, int userId)
        {
            if (dto.EndDate < dto.StartDate)
                throw new ArgumentException("End date cannot be before start date");

            if (dto.Budget < 0)
                throw new ArgumentException("Budget cannot be negative");

            TravelPlan travel = new TravelPlan
            {
                title = dto.Title,
                description = dto.Description,
                startDate = dto.StartDate,
                endDate = dto.EndDate,
                budget = dto.Budget,
                notes = dto.Notes,
                userId = userId,
                createdAt = DateTime.UtcNow
            };

            await context.TravelPlans.AddAsync(travel);
            await context.SaveChangesAsync();
            return travel;
        }

        public async Task<bool> deleteTravelPlan(int id,int userId)
        {
            var travel = await context.TravelPlans.FirstOrDefaultAsync(
                t => t.id == id && t.userId == userId);

            if (travel == null) return false;

            context.TravelPlans.Remove(travel);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TravelPlan>> getAllTravelPlans(int userId)
        {
            return await context.TravelPlans
                .Where(t => t.userId == userId)
                .ToListAsync();
        }

        public async Task<TravelPlan?> getTravelPlan(int id, int userId)
        {
            return await context.TravelPlans.FirstOrDefaultAsync(t => t.id == id
                && t.userId == userId);
        }

        public async Task<bool> updateTravelPlan(int id, UpdateTravelPlanDto dto, int userId)
        {
            var travel = await context.TravelPlans.FirstOrDefaultAsync(
                t => t.id == id && t.userId == userId);

            if (travel != null)
            {
                travel.title = dto.Title ?? travel.title;
                travel.description = dto.Description ?? travel.description;
                travel.startDate = dto.StartDate ?? travel.startDate;
                travel.endDate = dto.EndDate ?? travel.endDate;
                travel.budget = dto.Budget ?? travel.budget;
                travel.notes = dto.Notes ?? travel.notes;

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
