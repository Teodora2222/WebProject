using System.Xml;
using Microsoft.EntityFrameworkCore;
using TripService.Data;
using TripService.Domain.DTOs;
using TripService.Domain.Models;
using TripService.Domain.Services;

namespace TripService.Services
{
    public class ActivityService : IActivityService
    {
        private readonly AppDbContext context;

        public ActivityService(AppDbContext dbContext)
        {
            context = dbContext;
        }
        public async Task<Activity> createActivity(CreateActivityDto dto, int travelId)
        {
            Activity activity = new Activity
            {
                description = dto.Description,
                location = dto.Location,
                name = dto.Name,
                date = dto.Date,
                time = dto.Time,
                status = dto.status.ToString(),
                estimatedCost = dto.EstimatedCost,
                travelPlanId = travelId
            };

            await context.Activities.AddAsync(activity);
            await context.SaveChangesAsync();
            return activity;
        }

        public async Task<bool> deleteActivity(int id)
        {
            var activity = await context.Activities.FirstOrDefaultAsync(
                a => a.id == id);

            if (activity == null) return false;

            context.Activities.Remove(activity);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Activity>> getActivitiesByDate(int travelPlanId, DateTime date)
        {
            return await context.Activities
                    .Where(a => a.travelPlanId == travelPlanId && a.date.Date == date.Date)
                    .ToListAsync();
        }

        public async Task<Activity> getActivity(int id)
        {
            return await context.Activities.FirstOrDefaultAsync(a => a.id == id);
        }

        public async Task<List<Activity>> getAllActivities(int travelId)
        {
            return await context.Activities.Where(a => a.travelPlanId == travelId).ToListAsync();
        }

        public async Task<bool> updateActivity(int id, UpdateActivityDto dto)
        {
            var activity = await context.Activities.FirstOrDefaultAsync(a => a.id == id);

            if( activity != null)
            {
                activity.name = dto.Name ?? activity.name;
                activity.location = dto.Location ?? activity.location;
                activity.date = dto.Date ?? activity.date;
                activity.estimatedCost = dto.EstimatedCost ?? activity.estimatedCost;
                activity.time = dto.Time ?? activity.time;
                activity.description = dto.Description ?? activity.description;
                activity.status = dto.status?.ToString() ?? activity.status;

                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}

