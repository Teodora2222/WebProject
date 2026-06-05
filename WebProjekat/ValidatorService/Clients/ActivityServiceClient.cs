using Contract.Services;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace ValidatorService.Clients
{
    public class ActivityServiceClient
    {
        private static readonly Uri ServiceUri =
            new Uri("fabric:/WebProject/TripService");

        public IActivityService CreateProxy()
        {
            return ServiceProxy.Create<IActivityService>(
                ServiceUri);
        }
    }
}
