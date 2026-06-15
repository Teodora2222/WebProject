using Contract.Services;
using ExpenseService;
using ExpenseService.Clients;
using ExpenseService.Data;
using ExpenseService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Runtime;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.Development.json", optional: true)
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

var services = new ServiceCollection();
services.AddSingleton<IConfiguration>(configuration);

services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        configuration.GetConnectionString("DefaultConnection"));
    //options.UseSqlServer(
    //    "Server=TEODORA\\SQLEXPRESS01;Database=ExpensePlannerDB;Trusted_Connection=True;TrustServerCertificate=True;");
});

services.AddScoped<IExpenseService, ExpenseService.Services.ExpenseService>();

services.AddSingleton<ActivityServiceClient>();

var serviceProvider = services.BuildServiceProvider();

await ServiceRuntime.RegisterServiceAsync(
    "ExpenseServiceType",
    context => new ExpenseServiceHost(context, serviceProvider));

await Task.Delay(Timeout.InfiniteTimeSpan);