namespace QuantityMeasurement.Domain.Enums
{
    /// <summary>
    /// Represents supported length units and their conversion factors
    /// relative to base unit (Feet).
    /// </summary>
    public enum LengthUnit
    {
        Feet,
        Inches
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
                LengthUnit.Feet => 1.0, // Base unit
                LengthUnit.Inches => 1.0 / 12.0,  // 1 inch = 1/12 feet
                _ => throw new ArgumentException("Unsupported length unit.")
            };
        }
    }
}