using Contract.Services;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace ValidatorService.Clients
{
    public class DestinationServiceClient
    {
        private static readonly Uri ServiceUri =
            new Uri("fabric:/WebProject/TripService");

        public IDestinationService CreateProxy()
        {
            return ServiceProxy.Create<IDestinationService>(
                ServiceUri
                );
        }
    }
}
