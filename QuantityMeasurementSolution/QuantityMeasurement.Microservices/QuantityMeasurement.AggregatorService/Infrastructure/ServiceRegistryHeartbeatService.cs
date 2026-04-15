using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using QuantityMeasurement.AggregatorService.Configuration;

namespace QuantityMeasurement.AggregatorService.Infrastructure;

public sealed class ServiceRegistryHeartbeatService : BackgroundService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ServiceDiscoveryOptions _options;
    private readonly ILogger<ServiceRegistryHeartbeatService> _logger;

    public ServiceRegistryHeartbeatService(
        IHttpClientFactory httpClientFactory,
        IOptions<ServiceDiscoveryOptions> options,
        ILogger<ServiceRegistryHeartbeatService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(Math.Max(5, _options.TtlSeconds / 2)));

        await RegisterAsync(stoppingToken);

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            await RegisterAsync(stoppingToken);
        }
    }

    private async Task RegisterAsync(CancellationToken cancellationToken)
    {
        try
        {
            var payload = JsonSerializer.Serialize(
                new
                {
                    serviceName = _options.ServiceName,
                    serviceUrl = _options.ServiceUrl,
                    ttlSeconds = _options.TtlSeconds
                },
                JsonOptions);

            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            using var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync(
                $"{_options.RegistryUrl.TrimEnd('/')}/registry/register",
                content,
                cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Registry heartbeat failed with status code {StatusCode}.", response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Unable to register {ServiceName} with the service registry.", _options.ServiceName);
        }
    }
}
