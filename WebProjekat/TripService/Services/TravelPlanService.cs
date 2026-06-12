using System.Diagnostics;
using Contract.Dtos.Trip;
using Contract.Services;
using Microsoft.EntityFrameworkCore;
using TripService.Data;
using TripService.Domain.Models;

namespace TripService.Services
{
    public class TravelPlanService : ITravelPlanService
    {
        private readonly AppDbContext context;

        public TravelPlanService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<TravelPlanDto> createTravelPlan(CreateTravelPlanDto dto, int userId)
        {
            if (dto.EndDate < dto.StartDate)
                throw new ArgumentException("End date cannot be before start date");

            if (dto.Budget < 0)
                throw new ArgumentException("Budget cannot be negative");

            TravelPlan travel = new TravelPlan
            {
                title = dto.Title ?? "",
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
            return new TravelPlanDto
            {
                Id = travel.id,
                UserId = travel.userId,
                Title = travel.title,
                Description = travel.description ?? "",
                StartDate = travel.startDate,
                EndDate = travel.endDate,
                Budget = travel.budget,
                Notes = travel.notes ?? "",
                CreatedAt = travel.createdAt
            };
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

        public async Task<List<TravelPlanDto>> getAllTravelPlansAdmin()
        {
            return await context.TravelPlans
              .Select(t => new TravelPlanDto
            {
                Id = t.id,
                UserId = t.userId,
                Title = t.title,
                Description = t.description ?? "",
                StartDate = t.startDate,
                EndDate = t.endDate,
                Budget = t.budget,
                Notes = t.notes ?? "",
                CreatedAt = t.createdAt
            })
            .ToListAsync();
        }

        public async Task<List<TravelPlanDto>> getAllTravelPlans(int userId)
        {
            return await context.TravelPlans
                .Where(t => t.userId == userId)
                .Select(t => new TravelPlanDto
                {
                    Id = t.id,
                    UserId = t.userId,
                    Title = t.title,
                    Description = t.description ?? "",
                    StartDate = t.startDate,
                    EndDate = t.endDate,
                    Budget = t.budget,
                    Notes = t.notes ?? "",
                    CreatedAt = t.createdAt
                })
            .ToListAsync();
        }

        public async Task<TravelPlanDto?> getTravelPlan(int id, int userId)
        {
            var travel = await context.TravelPlans
                .FirstOrDefaultAsync(t => t.id == id && t.userId == userId);

            if (travel == null)
                return null;

            return new TravelPlanDto
            {
                Id = travel.id,
                UserId = travel.userId,
                Title = travel.title,
                Description = travel.description ?? "",
                StartDate = travel.startDate,
                EndDate = travel.endDate,
                Budget = travel.budget,
                Notes = travel.notes ?? "",
                CreatedAt = travel.createdAt
            };
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

        public async Task<TravelPlanDto?> getTravelPlanById(int id)
        {
            var travel = await context.TravelPlans
                .FirstOrDefaultAsync(t => t.id == id);

            if (travel == null)
                return null;

            return new TravelPlanDto
            {
                Id = travel.id,
                UserId = travel.userId,
                Title = travel.title,
                Description = travel.description ?? "",
                StartDate = travel.startDate,
                EndDate = travel.endDate,
                Budget = travel.budget,
                Notes = travel.notes ?? "",
                CreatedAt = travel.createdAt
            };
        }
        public async Task<bool> deleteTravelPlanAdmin(int id)
        {
            var travel = await context.TravelPlans.FirstOrDefaultAsync(t => t.id == id);
            if (travel == null) return false;
            context.TravelPlans.Remove(travel);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> updateTravelPlanAdmin(int id, UpdateTravelPlanDto dto)
        {
            var travel = await context.TravelPlans.FirstOrDefaultAsync(t => t.id == id);
            if (travel == null) return false;
            travel.title = dto.Title ?? travel.title;
            travel.description = dto.Description ?? travel.description;
            travel.startDate = dto.StartDate ?? travel.startDate;
            travel.endDate = dto.EndDate ?? travel.endDate;
            travel.budget = dto.Budget ?? travel.budget;
            travel.notes = dto.Notes ?? travel.notes;
            await context.SaveChangesAsync();
            return true;
        }
    }
}
