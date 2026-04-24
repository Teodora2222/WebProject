using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TripService.Data;
using TripService.Domain.DTOs;
using TripService.Domain.Models;
using TripService.Domain.Services;

namespace TripService.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly AppDbContext context;

        public DestinationService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Destination> createDestination(CreateDestinationDto dto, int travelPlanId)
        {
            if (dto.EndDate < dto.StartDate)
                throw new ArgumentException("End date cannot be before start date");

            Destination destination = new Destination
            {
                travelId = travelPlanId,
                name = dto.Name,
                description = dto.Description,
                startDate = dto.StartDate,
                endDate = dto.EndDate,
                location = dto.Location,
                note = dto.Note,
            };

            await context.Destinations.AddAsync(destination);
            await context.SaveChangesAsync();
            return destination;
        }

        public async Task<bool> deleteDestination(int id)
        {
            var destination = await context.Destinations.FirstOrDefaultAsync(
                t => t.id == id );

            if (destination == null) return false;

            context.Destinations.Remove(destination);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Destination>> getAllDestinastons(int travelPlanId)
        {
            return await context.Destinations.Where(d => d.travelId == travelPlanId).ToListAsync();
        }

        public async Task<Destination?> getDestination(int id)
        {
            return await context.Destinations.FirstOrDefaultAsync(d => d.id == id);
        }

        public async Task<bool> updateDestination(int id, UpdateDestinationDto dto)
        {
            var destination = await context.Destinations.FirstOrDefaultAsync(d => d.id == id);
            if (destination != null)
            {
                destination.name = dto.Name ?? destination.name;
                destination.description = dto.Description ?? destination.description;
                destination.startDate = dto.StartDate ?? destination.startDate;
                destination.endDate = dto.EndDate ?? destination.endDate;
                destination.location = dto.Location ?? destination.location;
                destination.note = dto.Note ?? destination.note;

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
