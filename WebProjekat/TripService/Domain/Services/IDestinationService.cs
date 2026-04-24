using TripService.Domain.DTOs;
using TripService.Domain.Models;

namespace TripService.Domain.Services
{
    public interface IDestinationService
    {
        public Task<List<Destination>> getAllDestinastons(int travelId);

        public Task<bool> deleteDestination(int id);

        public Task<bool> updateDestination(int id,UpdateDestinationDto dto);

        public Task<Destination> getDestination(int id);

        public Task<Destination> createDestination(CreateDestinationDto dto, int travelPlanId);
    }
}
