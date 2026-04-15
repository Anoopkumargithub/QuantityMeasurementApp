namespace QuantityMeasurement.QmaService.Contracts;

public sealed class ConvertRequest
{
    public QuantityRequest SourceQuantity { get; set; } = new();
    public string TargetUnit { get; set; } = string.Empty;
}
