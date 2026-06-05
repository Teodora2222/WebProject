using Contract.Services;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace ValidatorService.Clients
{
    public class ExpenseServiceClient
    {
        private static readonly Uri ExpenseServiceUri =
            new Uri("fabric:/WebProject/ExpenseService");

        public IExpenseService CreateProxy()
        {
            return ServiceProxy.Create<IExpenseService>(
                ExpenseServiceUri,
                new ServicePartitionKey(0));
        }
    }
}
