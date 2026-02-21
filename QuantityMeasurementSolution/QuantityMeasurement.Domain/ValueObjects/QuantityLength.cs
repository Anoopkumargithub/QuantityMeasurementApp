using System;
using QuantityMeasurement.Domain.Enums;

namespace QuantityMeasurement.Domain.ValueObjects
{
    /// <summary>
    /// Represents a generic length measurement with value and unit.
    /// Implements DRY principle by consolidating Feet and Inches logic.
    /// </summary>
    public sealed class QuantityLength : IEquatable<QuantityLength>
    {
        public double Value { get; }
        public LengthUnit Unit { get; }

        // Defining a small epsilon for floating-point comparison to handle precision issues in unit conversions.
        private const double Epsilon = 1e-6;

        /// <summary>
        /// Initializes a new instance of QuantityLength.
        /// </summary>
        public QuantityLength(double value, LengthUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Converts the quantity to base unit (Feet).
        /// </summary>
        private double ConvertToBase()
        {
            return Value * Unit.GetConversionFactor();
        }

        /// <summary>
        /// Value-based equality comparison using base conversion.
        /// </summary>
        public bool Equals(QuantityLength? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            double baseThis = Convert(Value, Unit, LengthUnit.Feet);
            double baseOther = Convert(other.Value, other.Unit, LengthUnit.Feet);

            return Math.Abs(baseThis - baseOther) < Epsilon;
        }

        public override bool Equals(object? obj)
        {
            return obj is QuantityLength other && Equals(other);
        }

        public override int GetHashCode()
        {
            return ConvertToBase().GetHashCode();
        }

        public static bool operator ==(QuantityLength? left, QuantityLength? right)
            => Equals(left, right);

        public static bool operator !=(QuantityLength? left, QuantityLength? right)
            => !Equals(left, right);

        public override string ToString()
            => $"{Value} {Unit}";

        /// <summary>
        /// Static API to convert value between units.
        /// </summary>
        public static double Convert(double value, LengthUnit source,       LengthUnit target)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            if (!Enum.IsDefined(typeof(LengthUnit), source))
                throw new ArgumentException("Invalid source unit.");

            if (!Enum.IsDefined(typeof(LengthUnit), target))
                throw new ArgumentException("Invalid target unit.");

            if (source == target)
                return value;

            double result = value * 
                (source.GetConversionFactor() / target.GetConversionFactor());

            return result;
        }

        /// <summary>
        /// Instance method conversion (returns new immutable object).
        /// </summary>
        public QuantityLength ConvertTo(LengthUnit target)
        {
            double convertedValue = Convert(Value, Unit, target);
            return new QuantityLength(convertedValue, target);
        }
    }
}