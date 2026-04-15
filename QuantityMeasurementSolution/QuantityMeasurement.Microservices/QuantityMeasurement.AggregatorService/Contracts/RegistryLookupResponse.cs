namespace QuantityMeasurement.AggregatorService.Contracts;

public sealed class RegistryLookupResponse
{
    public string ServiceName { get; set; } = string.Empty;
    public List<string> Instances { get; set; } = new();
}
