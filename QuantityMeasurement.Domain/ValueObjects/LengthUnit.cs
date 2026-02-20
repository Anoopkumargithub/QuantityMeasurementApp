using System;

namespace QuantityMeasurement.Domain.ValueObjects
{
    public enum LengthUnit
    {
        Feet = 1,
        Inches = 2,
        Yards = 3,
        Centimeters = 4
    }

    internal static class LengthUnitExtensions
    {
        private const double InchesToFeet = 1.0 / 12.0;               // 1 in = 1/12 ft
        private const double YardsToFeet = 3.0;                      // 1 yd = 3 ft
        private const double CentimetersToFeet = 0.393701 / 12.0;    // 1 cm = 0.393701 in

        public static double ToBaseFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Feet => 1.0,
                LengthUnit.Inches => InchesToFeet,
                LengthUnit.Yards => YardsToFeet,
                LengthUnit.Centimeters => CentimetersToFeet,
                _ => throw new ArgumentOutOfRangeException(nameof(unit), "Unsupported length unit.")
            };
        }
    }
}