using Contract.Services;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace Gateway.Clients
{
    public class ShareServiceClient
    {
        private static readonly Uri ServiceUri =
            new Uri("fabric:/WebProject/TripService");

        public IShareService CreateProxy()
        {
            return ServiceProxy.Create<IShareService>(
                ServiceUri,
                new ServicePartitionKey(0));
        }
    }
}
