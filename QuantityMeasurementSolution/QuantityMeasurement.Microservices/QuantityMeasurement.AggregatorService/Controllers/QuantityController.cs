using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QuantityMeasurement.AggregatorService.Configuration;
using QuantityMeasurement.AggregatorService.Contracts;
using QuantityMeasurement.AggregatorService.Services;

namespace QuantityMeasurement.AggregatorService.Controllers;

[ApiController]
[Route("api/quantity")]
public sealed class QuantityController : ControllerBase
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ServiceDiscoveryClient _serviceDiscoveryClient;
    private readonly ServiceDiscoveryOptions _options;

    public QuantityController(
        IHttpClientFactory httpClientFactory,
        ServiceDiscoveryClient serviceDiscoveryClient,
        IOptions<ServiceDiscoveryOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _serviceDiscoveryClient = serviceDiscoveryClient;
        _options = options.Value;
    }

    [HttpPost("compare")]
    public Task<ActionResult> Compare([FromBody] CompareRequest request, CancellationToken cancellationToken)
        => ProxyAsync("/api/operations/compare", request, cancellationToken);

    [HttpPost("convert")]
    public Task<ActionResult> Convert([FromBody] ConvertRequest request, CancellationToken cancellationToken)
        => ProxyAsync("/api/operations/convert", request, cancellationToken);

    [HttpPost("add")]
    public Task<ActionResult> Add([FromBody] ArithmeticRequest request, CancellationToken cancellationToken)
        => ProxyAsync("/api/operations/add", request, cancellationToken);

    [HttpPost("subtract")]
    public Task<ActionResult> Subtract([FromBody] ArithmeticRequest request, CancellationToken cancellationToken)
        => ProxyAsync("/api/operations/subtract", request, cancellationToken);

    [HttpPost("divide")]
    public Task<ActionResult> Divide([FromBody] ArithmeticRequest request, CancellationToken cancellationToken)
        => ProxyAsync("/api/operations/divide", request, cancellationToken);

    [HttpGet("health")]
    public Task<ActionResult> Health(CancellationToken cancellationToken)
        => ProxyGetAsync("/api/operations/health", cancellationToken);

    private async Task<ActionResult> ProxyAsync(string downstreamPath, object payload, CancellationToken cancellationToken)
    {
        var baseUrl = await _serviceDiscoveryClient.GetServiceBaseUrlAsync(_options.OperationServiceName, cancellationToken);
        var targetUrl = $"{baseUrl}{downstreamPath}";

        using var client = _httpClientFactory.CreateClient();
        using var requestContent = new StringContent(
            JsonSerializer.Serialize(payload, JsonOptions),
            Encoding.UTF8,
            "application/json");

        using var response = await client.PostAsync(targetUrl, requestContent, cancellationToken);
        var raw = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return Content(raw, "application/json", Encoding.UTF8);
        }

        return StatusCode((int)response.StatusCode, raw);
    }

    private async Task<ActionResult> ProxyGetAsync(string downstreamPath, CancellationToken cancellationToken)
    {
        var baseUrl = await _serviceDiscoveryClient.GetServiceBaseUrlAsync(_options.OperationServiceName, cancellationToken);
        var targetUrl = $"{baseUrl}{downstreamPath}";

        using var client = _httpClientFactory.CreateClient();
        using var response = await client.GetAsync(targetUrl, cancellationToken);
        var raw = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return Content(raw, "application/json", Encoding.UTF8);
        }

        return response.StatusCode == HttpStatusCode.NotFound
            ? NotFound(raw)
            : StatusCode((int)response.StatusCode, raw);
    }
}
