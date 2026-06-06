using Contract.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Runtime;
using TripService;
using TripService.Data;
using TripService.Services;

var services = new ServiceCollection();

services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        "Server=TEODORA\\SQLEXPRESS01;Database=TravelPlannerDB;Trusted_Connection=True;TrustServerCertificate=True;");
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