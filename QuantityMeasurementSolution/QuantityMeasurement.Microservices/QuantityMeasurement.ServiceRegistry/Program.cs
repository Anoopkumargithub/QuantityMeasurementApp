using QuantityMeasurement.ServiceRegistry.Models;
using QuantityMeasurement.ServiceRegistry.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IServiceRegistryStore, InMemoryServiceRegistryStore>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/registry/register", (ServiceRegistrationRequest request, IServiceRegistryStore registry) =>
{
    if (string.IsNullOrWhiteSpace(request.ServiceName) ||
        string.IsNullOrWhiteSpace(request.ServiceUrl))
    {
        return Results.BadRequest(new { message = "ServiceName and ServiceUrl are required." });
    }

    registry.Register(request);
    return Results.Ok(new { message = "Service registered successfully." });
});

app.MapGet("/registry/services/{serviceName}", (string serviceName, IServiceRegistryStore registry) =>
{
    var urls = registry.GetServiceUrls(serviceName);

    return urls.Count == 0
        ? Results.NotFound(new { message = $"No active instance found for '{serviceName}'." })
        : Results.Ok(new { serviceName, instances = urls });
});

app.MapGet("/registry/all", (IServiceRegistryStore registry) =>
{
    return Results.Ok(registry.GetAll());
});

app.MapGet("/health", () => Results.Ok(new { status = "OK" }));

app.Run();
