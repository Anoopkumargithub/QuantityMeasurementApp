namespace QuantityMeasurement.AggregatorService.Configuration;

public sealed class ServiceDiscoveryOptions
{
    public const string SectionName = "ServiceDiscovery";

    public string RegistryUrl { get; set; } = "http://localhost:7000";
    public string ServiceName { get; set; } = "quantity-aggregator-service";
    public string ServiceUrl { get; set; } = "http://localhost:7002";
    public int TtlSeconds { get; set; } = 30;

    public string OperationServiceName { get; set; } = "quantity-operation-service";
}
