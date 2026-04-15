using QuantityMeasurement.AggregatorService.Configuration;
using QuantityMeasurement.AggregatorService.Infrastructure;
using QuantityMeasurement.AggregatorService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddOptions<ServiceDiscoveryOptions>()
    .Bind(builder.Configuration.GetRequiredSection(ServiceDiscoveryOptions.SectionName))
    .ValidateDataAnnotations();

builder.Services.AddHttpClient();
builder.Services.AddScoped<ServiceDiscoveryClient>();
builder.Services.AddHostedService<ServiceRegistryHeartbeatService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
