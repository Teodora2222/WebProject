using System.Diagnostics;
using Contract.Dtos.Trip;
using Contract.Services;
using Microsoft.EntityFrameworkCore;
using TripService.Data;
using TripService.Domain.Models;

namespace TripService.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly AppDbContext context;

        public DestinationService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<DestinationDto> createDestination(CreateDestinationDto dto, int travelPlanId)
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
            return new DestinationDto
            {
                Id = destination.id,
                TravelId = destination.travelId,
                Name = destination.name,
                Location = destination.location ?? "",
                StartDate = destination.startDate,
                EndDate = destination.endDate,
                Description = destination.description ?? "",
                Note = destination.note ?? ""
            };
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

        public async Task<List<DestinationDto>> getAllDestinastons(int travelPlanId)
        {
            return await context.Destinations
                .Where(d => d.travelId == travelPlanId)
                .Select(d => new DestinationDto
                {
                    Id = d.id,
                    TravelId = d.travelId,
                    Name = d.name,
                    Location = d.location ?? "",
                    StartDate = d.startDate,
                    EndDate = d.endDate,
                    Description = d.description ?? "",
                    Note = d.note ?? ""
                })
                .ToListAsync();
        }

        public async Task<DestinationDto?> getDestination(int id)
        {
            var destination = await context.Destinations
                .FirstOrDefaultAsync(d => d.id == id);

            if (destination == null)
                return null;

            return new DestinationDto
            {
                Id = destination.id,
                TravelId = destination.travelId,
                Name = destination.name,
                Location = destination.location ?? "",
                StartDate = destination.startDate,
                EndDate = destination.endDate,
                Description = destination.description ?? "",
                Note = destination.note ?? ""
            };
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
