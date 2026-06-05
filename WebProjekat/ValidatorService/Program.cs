using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Runtime;
using ValidatorService.Clients;
using ValidatorService.Validators;
using ValidatorService;

namespace ValidatorService
{
    internal static class Program
    {
        /// <summary>
        /// This is the entry point of the service host process.
        /// </summary>
        private static void Main()
        {
            try
            {
                var userClient = new UserServiceClient();
                var travelClient = new TravelPlanServiceClient();
                var destinationClient = new DestinationServiceClient();
                var activityClient = new ActivityServiceClient();
                var checklistClient = new ChecklistServiceClient();
                var shareClient = new ShareServiceClient();
                var expenseClient = new ExpenseServiceClient();

                var userValidator = new UserValidator(userClient);

                var travelValidator = new TravelValidator(
                    travelClient,
                    destinationClient,
                    activityClient,
                    checklistClient,
                    shareClient);

                var expenseValidator = new ExpenseValidator(expenseClient);

                ServiceRuntime.RegisterServiceAsync(
                    "ValidatorServiceType",
                    context => new ValidatorService(
                        context,
                        userValidator,
                        travelValidator,
                        expenseValidator))
                    .GetAwaiter()
                    .GetResult();

                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                ServiceEventSource.Current.ServiceHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
