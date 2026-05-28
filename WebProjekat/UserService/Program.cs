using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.ServiceFabric.Services.Runtime;
using UserService;
using UserService.Data;
using UserService.Domain.Services;
using UserService.Services;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("Fabric_Endpoint_ServiceEndpoint") ?? "8275";
builder.WebHost.UseUrls($"http://+:{port}");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
    "Server=TEODORA\\SQLEXPRESS01;Database=UserPlannerDB;Trusted_Connection=True;TrustServerCertificate=True;");
   
    //var connStr = builder.Configuration.GetConnectionString("DefaultConnection")
    //  ?? "Server=TEODORA\\SQLEXPRESS01;Database=TravelPlannerDB;Trusted_Connection=True;TrustServerCertificate=True;";
    //options.UseSqlServer(connStr);
});

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IUserService, UserService.Services.UserService>();

var jwtKey = "TvojTajniKljucKojiMoraBitiDugacak32Karaktera!";
var jwtIssuer = "TravelPlannerApp";


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

Task.Run(async () =>
{
    await ServiceRuntime.RegisterServiceAsync("UserServiceType",
        context => new UserServiceHost(context));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();  