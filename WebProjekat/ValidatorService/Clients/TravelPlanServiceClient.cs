using Contract.Services;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace ValidatorService.Clients
{
    public class TravelPlanServiceClient
    {
        private static readonly Uri TravelServiceUri =
            new Uri("fabric:/WebProject/TripService");

        public ITravelPlanService CreateProxy()
        {
            return ServiceProxy.Create<ITravelPlanService>(
                TravelServiceUri
               );
        }
    }
}
