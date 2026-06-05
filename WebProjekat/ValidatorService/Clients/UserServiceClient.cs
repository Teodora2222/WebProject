using Contract.Services;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace ValidatorService.Clients
{
    public class UserServiceClient
    {
        private static readonly Uri UserServiceUri =
           new Uri("fabric:/WebProject/UserService");

        public IUserService CreateProxy()
        {
            return ServiceProxy.Create<IUserService>(
                UserServiceUri
                );
        }
    }
}
