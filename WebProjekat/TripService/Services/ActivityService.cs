using System.Xml;
using Contract.Dtos.Trip;
using Contract.Services;
using Microsoft.EntityFrameworkCore;
using TripService.Data;
using TripService.Domain.Models;

namespace TripService.Services
{
    public class ActivityService : IActivityService
    {
        private readonly AppDbContext context;

        public ActivityService(AppDbContext dbContext)
        {
            context = dbContext;
        }
        public async Task<ActivityDto> createActivity(CreateActivityDto dto, int travelId)
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
            return new ActivityDto
            {
                Id = activity.id,
                TravelPlanId = activity.travelPlanId,
                Name = activity.name,
                Date = activity.date,
                Time = activity.time,
                Location = activity.location,
                Description = activity.description,
                EstimatedCost = activity.estimatedCost,
                Status = activity.status
            };
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

        public async Task<List<ActivityDto>> getActivitiesByDate(int travelPlanId, DateTime date)
        {
            return await context.Activities
            .Where(a => a.travelPlanId == travelPlanId &&
                     a.date.Date == date.Date)
         .Select(a => new ActivityDto
         {
             Id = a.id,
             TravelPlanId = a.travelPlanId,
             Name = a.name,
             Date = a.date,
             Time = a.time,
             Location = a.location,
             Description = a.description,
             EstimatedCost = a.estimatedCost,
             Status = a.status
         })
         .ToListAsync();
        }

        public async Task<ActivityDto> getActivity(int id)
        {
            var activity = await context.Activities
                .FirstOrDefaultAsync(a => a.id == id);

            if (activity == null)
                return null;

            return new ActivityDto
            {
                Id = activity.id,
                TravelPlanId = activity.travelPlanId,
                Name = activity.name,
                Date = activity.date,
                Time = activity.time,
                Location = activity.location,
                Description = activity.description,
                EstimatedCost = activity.estimatedCost,
                Status = activity.status
            };
        }

        public async Task<List<ActivityDto>> getAllActivities(int travelId)
        {
            return await context.Activities
       .Where(a => a.travelPlanId == travelId)
       .Select(a => new ActivityDto
       {
           Id = a.id,
           TravelPlanId = a.travelPlanId,
           Name = a.name,
           Date = a.date,
           Time = a.time,
           Location = a.location,
           Description = a.description,
           EstimatedCost = a.estimatedCost,
           Status = a.status
       })
       .ToListAsync();
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

