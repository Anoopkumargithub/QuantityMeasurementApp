using System;

namespace QuantityMeasurement.Domain.Enums
{
    /// <summary>
    /// Supported length units with conversion factors relative to FEET.
    /// </summary>
    public enum LengthUnit
    {
        Feet,
        Inches,
        Yards,
        Centimeters
    }

    public static class LengthUnitExtensions
    {
        /// <summary>
        /// Gets conversion factor to base unit (Feet).
        /// </summary>
        public static double GetConversionFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Feet => 1.0,     // Base unit
                LengthUnit.Inches => 1.0 / 12.0,        // 1 foot = 12 inches
                LengthUnit.Yards => 3.0,        // 1 yard = 3 feet
                LengthUnit.Centimeters => 0.393701 / 12.0,      // 1 inch = 2.54 cm => 1 cm = 0.393701 inches => 1 cm = 0.393701/12 feet
                _ => throw new ArgumentException("Unsupported length unit.")
            };
        }
    }
}