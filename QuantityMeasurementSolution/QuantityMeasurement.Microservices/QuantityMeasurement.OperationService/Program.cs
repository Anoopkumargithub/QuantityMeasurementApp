using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using QuantityMeasurement.OperationService.Configuration;
using QuantityMeasurement.OperationService.Infrastructure;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IQuantityMeasurementRepository, QuantityMeasurementCacheRepository>();
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementService>();

builder.Services
    .AddOptions<ServiceDiscoveryOptions>()
    .Bind(builder.Configuration.GetRequiredSection(ServiceDiscoveryOptions.SectionName))
    .ValidateDataAnnotations();

builder.Services.AddHttpClient();
builder.Services.AddHostedService<ServiceRegistryHeartbeatService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
