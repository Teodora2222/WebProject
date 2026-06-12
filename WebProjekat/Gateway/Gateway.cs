using System.Fabric;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Gateway.Clients;
using Gateway.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace Gateway
{
    internal sealed class Gateway : StatelessService
    {
        public Gateway(StatelessServiceContext context)
            : base(context)
        {
        }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new[]
            {
                new ServiceInstanceListener(serviceContext =>
                    new KestrelCommunicationListener(
                        serviceContext,
                        "ServiceEndpoint",
                        (url, listener) =>
                        {
                            var builder = WebApplication.CreateBuilder();

                            builder.Services.AddSingleton(serviceContext);

                            builder.WebHost
                                .UseKestrel()
                                .UseContentRoot(Directory.GetCurrentDirectory())
                                .UseServiceFabricIntegration(
                                    listener,
                                    ServiceFabricIntegrationOptions.None)
                                .UseUrls(url);


                            builder.Services.AddSingleton<ValidatorServiceClient>();
                            builder.Services.AddSingleton<UserServiceClient>();
                            builder.Services.AddSingleton<ActivityServiceClient>();
                            builder.Services.AddSingleton<TravelPlanServiceClient>();
                            builder.Services.AddSingleton<DestinationServiceClient>();
                            builder.Services.AddSingleton<ChecklistServiceClient>();
                            builder.Services.AddSingleton<ExpenseServiceClient>();
                            builder.Services.AddSingleton<ShareServiceClient>();


                            builder.Services.AddScoped<IAuthGatewayService,
                                ServiceFabricAuthGatewayService>();

                            builder.Services.AddScoped<ITravelGatewayService,
                                ServiceFabricTravelGatewayService>();

                            builder.Services.AddScoped<IExpenseGatewayService,
                                ServiceFabricExpenseGatewayService>();

                            builder.Services.AddScoped<IUserGatewayService,
                                ServiceFabricUserGatewayService>(); 


                            var jwtKey =
                                "TvojTajniKljucKojiMoraBitiDugacak32Karaktera!";

                            var jwtIssuer =
                                "TravelPlannerApp";

                            builder.Services
                                .AddAuthentication(
                                    JwtBearerDefaults.AuthenticationScheme)
                                .AddJwtBearer(options =>
                                {
                                    options.TokenValidationParameters =
                                        new TokenValidationParameters
                                        {
                                            ValidateIssuer = true,
                                            ValidateAudience = true,
                                            ValidateLifetime = true,
                                            ValidateIssuerSigningKey = true,

                                            ValidIssuer = jwtIssuer,
                                            ValidAudience = jwtIssuer,

                                            IssuerSigningKey =
                                                new SymmetricSecurityKey(
                                                    Encoding.UTF8.GetBytes(jwtKey))
                                        };
                                });

                            builder.Services.AddAuthorization();

                            builder.Services.AddCors(options =>
                            {
                                options.AddPolicy("Frontend", policy =>
                                {
                                    policy.WithOrigins("http://localhost:5173",
                                         "http://192.168.1.7:5173" )
                                          .AllowAnyHeader()
                                          .AllowAnyMethod();
                                });
                            });

                            builder.Services.AddControllers();
                            builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gateway API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                        Enter 'Bearer' [space] and then your token.
                        Example: Bearer eyJhbGciOiJIUzI1NiIs...",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

                            var app = builder.Build();

                            app.UseSwagger();
                            app.UseSwaggerUI();

                            app.UseCors("Frontend");

                            app.UseAuthentication();
                            app.UseAuthorization();

                            app.MapControllers();

                            return app;
                        }))
            };
        }
    }
}