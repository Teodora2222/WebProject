using Contract.Services;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace ValidatorService.Clients
{
    public class ChecklistServiceClient
    {
        private static readonly Uri ServiceUri =
            new Uri("fabric:/WebProject/TripService");

        public ICheckListItemService CreateProxy()
        {
            return ServiceProxy.Create<ICheckListItemService>(
                ServiceUri);
        }
    }
}
