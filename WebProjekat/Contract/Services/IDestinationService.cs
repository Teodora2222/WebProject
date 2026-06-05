using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Dtos.Trip;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Contract.Services
{
    public interface IDestinationService : IService
    {
        public Task<List<DestinationDto>> getAllDestinastons(int travelId);

        public Task<bool> deleteDestination(int id);

        public Task<bool> updateDestination(int id, UpdateDestinationDto dto);

        public Task<DestinationDto> getDestination(int id);

        public Task<DestinationDto> createDestination(CreateDestinationDto dto, int travelPlanId);
    }
}
