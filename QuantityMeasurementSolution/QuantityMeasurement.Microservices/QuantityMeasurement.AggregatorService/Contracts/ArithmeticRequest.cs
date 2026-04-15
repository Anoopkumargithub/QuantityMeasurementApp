namespace QuantityMeasurement.AggregatorService.Contracts;

public sealed class ArithmeticRequest
{
    public QuantityRequest FirstQuantity { get; set; } = new();
    public QuantityRequest SecondQuantity { get; set; } = new();
    public string? TargetUnit { get; set; }
}
