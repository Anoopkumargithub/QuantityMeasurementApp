using System;

namespace QuantityMeasurement.Domain.ValueObjects
{
    /// <summary>
    /// Enumeration representing different units of length measurement.
    /// This enum is used to specify the unit of measurement for length values in the QuantityLength class.
    /// Each member of the enum corresponds to a specific unit of length, such as feet,
    /// inches, yards, and centimeters. The enum values are assigned integer values starting from 1 for better readability and maintainability.
    /// The LengthUnit enum is designed to be used in conjunction with the QuantityLength class, which handles the actual measurement logic, including unit conversions and comparisons.
    /// </summary>
    /// <remarks>
    /// The LengthUnit enum provides a clear and type-safe way to specify the unit of measurement for length values, ensuring that the QuantityLength class can perform accurate conversions and comparisons based on the specified unit.
    /// The enum values are defined in a way that allows for easy extension in the future if additional length units need to be supported, without affecting the existing code that relies on the current set of units.
    /// The LengthUnit enum is an integral part of the domain model for quantity measurement, as it allows for consistent handling of different length units across the application, ensuring that all length measurements are properly represented and can be compared and converted as needed.
    /// </remarks>


    public enum LengthUnit
    {
        Feet = 1,
        Inches = 2,
        Yards = 3,
        Centimeters = 4
    }

    internal static class LengthUnitExtensions
    {
        // Defines conversion factors from each length unit to the base unit (feet) for accurate conversions and comparisons in the QuantityLength class.
        private const double InchesToFeet = 1.0 / 12.0;               // 1 in = 1/12 ft
        private const double YardsToFeet = 3.0;                      // 1 yd = 3 ft
        private const double CentimetersToFeet = 0.393701 / 12.0;    // 1 cm = 0.393701 in

        // Extension method to get the conversion factor from the specified length unit to the base unit (feet), allowing for accurate conversions and comparisons in the QuantityLength class.
        /// <param name="unit">The length unit for which to get the conversion factor to feet.</param>
        /// <returns>The conversion factor from the specified length unit to feet.</returns>
        /// <remarks>This method uses a switch expression to return the appropriate conversion factor based on the provided length unit, ensuring that the QuantityLength class can perform accurate conversions and comparisons regardless of the unit specified.</remarks>
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