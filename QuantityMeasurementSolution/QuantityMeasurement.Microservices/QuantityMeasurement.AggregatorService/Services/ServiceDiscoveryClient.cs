using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using QuantityMeasurement.AggregatorService.Configuration;
using QuantityMeasurement.AggregatorService.Contracts;

namespace QuantityMeasurement.AggregatorService.Services;

public sealed class ServiceDiscoveryClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ServiceDiscoveryOptions _options;

    public ServiceDiscoveryClient(IHttpClientFactory httpClientFactory, IOptions<ServiceDiscoveryOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
    }

    public async Task<string> GetServiceBaseUrlAsync(string serviceName, CancellationToken cancellationToken)
    {
        using var client = _httpClientFactory.CreateClient();
        var endpoint = $"{_options.RegistryUrl.TrimEnd('/')}/registry/services/{serviceName}";
        var lookup = await client.GetFromJsonAsync<RegistryLookupResponse>(endpoint, cancellationToken);

        var baseUrl = lookup?.Instances?.FirstOrDefault();
        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            throw new InvalidOperationException($"No healthy instance found for service '{serviceName}'.");
        }

        return baseUrl.TrimEnd('/');
    }
}
