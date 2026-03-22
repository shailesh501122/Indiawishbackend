using System.Reflection;
using System.Text;
using AspNetCoreRateLimit;
using BuildingBlocks.Application.Behaviors;
using BuildingBlocks.Application.Interfaces;
using BuildingBlocks.Infrastructure.Identity;
using BuildingBlocks.Infrastructure.Persistence;
using BuildingBlocks.Infrastructure.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "IndiaWish API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { Name = "Authorization", Type = SecuritySchemeType.Http, Scheme = "bearer", BearerFormat = "JWT", In = ParameterLocation.Header });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement { { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, Array.Empty<string>() } });
});
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.Load("Users.Application"), Assembly.Load("Properties.Application"), Assembly.Load("Listings.Application"), Assembly.Load("Bookings.Application"), Assembly.Load("Payments.Application"), Assembly.Load("Subscriptions.Application"), Assembly.Load("Reviews.Application"), Assembly.Load("Notifications.Application"), Assembly.Load("Search.Application")));
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IPaymentGateway, MockPaymentGateway>();
builder.Services.AddScoped<INotificationSender, CompositeNotificationSender>();
builder.Services.AddStackExchangeRedisCache(options => options.Configuration = builder.Configuration.GetConnectionString("Redis"));
builder.Services.AddMemoryCache();
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
var jwtSection = builder.Configuration.GetSection("Jwt");
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Secret"]!));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters { ValidateIssuer = true, ValidateAudience = true, ValidateIssuerSigningKey = true, ValidIssuer = jwtSection["Issuer"], ValidAudience = jwtSection["Audience"], IssuerSigningKey = key };
});
builder.Services.AddAuthorization();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseSerilogRequestLogging();
app.UseIpRateLimiting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/health", () => Results.Ok(new { Status = "Healthy" }));
app.Run();
public partial class Program;
