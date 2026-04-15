namespace QuantityMeasurement.QmaService.Contracts;

public sealed class CompareRequest
{
    public QuantityRequest FirstQuantity { get; set; } = new();
    public QuantityRequest SecondQuantity { get; set; } = new();
}
