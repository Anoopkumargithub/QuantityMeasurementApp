using System;

namespace QuantityMeasurement.Domain.ValueObjects
{
    public enum LengthUnit
    {
        Feet = 1,
        Inches = 2
    }

    internal static class LengthUnitExtensions
    {
        public static double ToBaseFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Feet => 1.0,
                LengthUnit.Inches => 1.0 / 12.0,
                _ => throw new ArgumentOutOfRangeException(nameof(unit), "Unsupported length unit.")
            };
        }
    }
}