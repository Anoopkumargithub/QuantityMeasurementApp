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

            return ConvertToBase().CompareTo(other.ConvertToBase()) == 0;
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
    }
}