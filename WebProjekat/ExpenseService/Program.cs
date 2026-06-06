using Contract.Services;
using ExpenseService;
using ExpenseService.Clients;
using ExpenseService.Data;
using ExpenseService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Runtime;

var services = new ServiceCollection();

services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        "Server=TEODORA\\SQLEXPRESS01;Database=ExpensePlannerDB;Trusted_Connection=True;TrustServerCertificate=True;");
});

services.AddScoped<IExpenseService, ExpenseService.Services.ExpenseService>();

services.AddSingleton<ActivityServiceClient>();

var serviceProvider = services.BuildServiceProvider();

await ServiceRuntime.RegisterServiceAsync(
    "ExpenseServiceType",
    context => new ExpenseServiceHost(context, serviceProvider));

await Task.Delay(Timeout.InfiniteTimeSpan);