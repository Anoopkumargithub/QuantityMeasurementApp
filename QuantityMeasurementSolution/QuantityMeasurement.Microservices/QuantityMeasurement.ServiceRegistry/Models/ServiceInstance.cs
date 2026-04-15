namespace QuantityMeasurement.ServiceRegistry.Models;

public sealed class ServiceInstance
{
    public string ServiceName { get; init; } = string.Empty;
    public string ServiceUrl { get; init; } = string.Empty;
    public DateTime LastHeartbeatUtc { get; set; }
    public int TtlSeconds { get; init; }
}
