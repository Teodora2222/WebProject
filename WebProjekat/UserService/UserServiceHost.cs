using System.Fabric;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace UserService
{
    internal sealed class UserServiceHost : StatelessService
    {
        public UserServiceHost(StatelessServiceContext context)
            : base(context) { }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[0];
        }
    }
}