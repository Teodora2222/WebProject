using Contract.Services;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace ExpenseService.Clients
{
    public class ActivityServiceClient
    {
        private static readonly Uri ActivityServiceUri =
            new Uri("fabric:/WebProject/TripService");

        public IActivityService CreateProxy()
        {
            return ServiceProxy.Create<IActivityService>(
                ActivityServiceUri);
        }
    }
}