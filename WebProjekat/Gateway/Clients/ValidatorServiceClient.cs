using Contract.Services;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace Gateway.Clients
{
    public class ValidatorServiceClient
    {
        private static readonly Uri ValidatorServiceUri =
            new ("fabric:/WebProject/ValidatorService");

        public IValidatorService CreateProxy()
        {
            return ServiceProxy.Create<IValidatorService>(ValidatorServiceUri);
        }
    }
}
