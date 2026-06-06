using Contract.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Runtime;
using UserService;
using UserService.Data;
using Microsoft.Extensions.Configuration;

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
        "Server=TEODORA\\SQLEXPRESS01;Database=UserPlannerDB;Trusted_Connection=True;TrustServerCertificate=True;");
});
services.AddScoped<IUserService, UserService.Services.UserService>();

var serviceProvider = services.BuildServiceProvider();

await ServiceRuntime.RegisterServiceAsync(
    "UserServiceType",
    context => new UserServiceHost(context, serviceProvider));

await Task.Delay(Timeout.InfiniteTimeSpan);