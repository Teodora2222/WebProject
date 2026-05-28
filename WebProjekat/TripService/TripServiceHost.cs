using System.Fabric;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace TripService
{
        internal sealed class TripServiceHost : StatelessService
        {
            public TripServiceHost(StatelessServiceContext context)
                : base(context) { }

            protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
            {
                return new ServiceInstanceListener[0];
            }
        }
}
