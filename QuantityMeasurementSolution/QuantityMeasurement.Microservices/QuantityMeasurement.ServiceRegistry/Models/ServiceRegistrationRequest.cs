namespace QuantityMeasurement.ServiceRegistry.Models;

public sealed class ServiceRegistrationRequest
{
    public string ServiceName { get; set; } = string.Empty;
    public string ServiceUrl { get; set; } = string.Empty;
    public int TtlSeconds { get; set; } = 30;
}
