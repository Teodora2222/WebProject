using Contract.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Runtime;
using TripService;
using TripService.Data;
using TripService.Services;

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
      //  "Server=TEODORA\\SQLEXPRESS01;Database=TravelPlannerDB;Trusted_Connection=True;TrustServerCertificate=True;");
});

services.AddScoped<ITravelPlanService, TravelPlanService>();
services.AddScoped<IDestinationService, DestinationService>();
services.AddScoped<IActivityService, ActivityService>();
services.AddScoped<ICheckListItemService, CheckListItemService>();
services.AddScoped<IShareService, ShareService>();

var serviceProvider = services.BuildServiceProvider();

await ServiceRuntime.RegisterServiceAsync(
    "TripServiceType",
    context => new TripServiceHost(context, serviceProvider));

await Task.Delay(Timeout.InfiniteTimeSpan);