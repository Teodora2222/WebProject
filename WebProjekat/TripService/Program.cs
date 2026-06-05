using System.Text;
using Contract.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.ServiceFabric.Services.Runtime;
using TripService;
using TripService.Data;
using TripService.Services;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("Fabric_Endpoint_ServiceEndpoint") ?? "8237";
builder.WebHost.UseUrls($"http://+:{port}");


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
    "Server=TEODORA\\SQLEXPRESS01;Database=TravelPlannerDB;Trusted_Connection=True;TrustServerCertificate=True;");
   
});

builder.Services.AddScoped<ITravelPlanService, TravelPlanService>();

builder.Services.AddScoped<IDestinationService, DestinationService>();

builder.Services.AddScoped<IActivityService, ActivityService>();

builder.Services.AddScoped<ICheckListItemService, CheckListItemService>();

builder.Services.AddScoped<IShareService, ShareService>();

var jwtKey = builder.Configuration["Jwt:Key"]
    ?? "TvojTajniKljucKojiMoraBitiDugacak32Karaktera!";
var jwtIssuer = builder.Configuration["Jwt:Issuer"]
    ?? "TravelPlannerApp";

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

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Unesi token: Bearer {token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var serviceProvider = builder.Services.BuildServiceProvider();

ServiceRuntime.RegisterServiceAsync("TripServiceType",
    context => new TripServiceHost(context, serviceProvider))
    .GetAwaiter().GetResult();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();