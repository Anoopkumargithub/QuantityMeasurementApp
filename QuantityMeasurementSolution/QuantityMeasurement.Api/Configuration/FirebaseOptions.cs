using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurement.Api.Configuration;

public sealed class FirebaseOptions
{
    public const string SectionName = "Firebase";

    [Required]
    public string ProjectId { get; init; } = string.Empty;
}
