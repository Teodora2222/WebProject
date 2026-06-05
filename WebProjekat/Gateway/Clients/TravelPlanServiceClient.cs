using Contract.Services;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace Gateway.Clients
{
    public class TravelPlanServiceClient
    {
        private static readonly Uri TravelServiceUri =
            new Uri("fabric:/WebProject/TripService");

        public ITravelPlanService CreateProxy()
        {
            return ServiceProxy.Create<ITravelPlanService>(
                TravelServiceUri,
                new ServicePartitionKey(0));
        }
    }
}
