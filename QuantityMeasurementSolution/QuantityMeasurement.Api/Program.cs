using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuantityMeasurement.RepositoryLayer.Data;
using QuantityMeasurement.RepositoryLayer.Repositories;
using QuantityMeasurement.Api.Configuration;
using RepositoryLayer.Interfaces;
using QuantityMeasurement.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var corsOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>();

if (corsOrigins is null || corsOrigins.Length == 0 || Array.Exists(corsOrigins, string.IsNullOrWhiteSpace))
{
    throw new InvalidOperationException("CORS origins are missing. Configure 'Cors:AllowedOrigins' with at least one valid origin.");
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(corsOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var connectionString = builder.Configuration.GetConnectionString("QuantityMeasurementDb")
    ?? throw new InvalidOperationException("Connection string 'QuantityMeasurementDb' is missing.");

builder.Services
    .AddOptions<FirebaseOptions>()
    .Bind(builder.Configuration.GetRequiredSection(FirebaseOptions.SectionName))
    .ValidateDataAnnotations()
    .Validate(
        options => !string.IsNullOrWhiteSpace(options.ProjectId),
        $"'{FirebaseOptions.SectionName}:ProjectId' must be configured.")
    .ValidateOnStart();

var firebaseOptions = builder.Configuration
    .GetRequiredSection(FirebaseOptions.SectionName)
    .Get<FirebaseOptions>()
    ?? throw new InvalidOperationException($"Configuration section '{FirebaseOptions.SectionName}' is missing.");

var firebaseProjectId = firebaseOptions.ProjectId;

builder.Services.AddDbContext<QuantityMeasurementDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://securetoken.google.com/{firebaseProjectId}";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = $"https://securetoken.google.com/{firebaseProjectId}",
            ValidateAudience = true,
            ValidAudience = firebaseProjectId,
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization(options =>
{
    // Keep authorization services registered; require auth only on endpoints marked with [Authorize].
});

builder.Services.AddScoped<IQuantityMeasurementRepository, QuantityMeasurementOrmRepository>();

builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementService>();

var app = builder.Build();
app.UseCors("AllowFrontend");
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
