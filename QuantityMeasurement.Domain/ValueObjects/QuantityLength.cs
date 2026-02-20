using System;

namespace QuantityMeasurement.Domain.ValueObjects
{
    public sealed class QuantityLength 
        : IEquatable<QuantityLength>, IComparable<QuantityLength>
    {
        private const double Tolerance = 0.0001;

        public double Value { get; }
        public LengthUnit Unit { get; }

        public QuantityLength(double value, LengthUnit unit)
        {
            if (double.IsNaN(value))
                throw new ArgumentException("Value cannot be NaN.", nameof(value));

            if (double.IsInfinity(value))
                throw new ArgumentException("Value cannot be Infinity.", nameof(value));

            Value = value;
            Unit = unit;
        }

        private double ToBaseUnit()
        {
            return Value * Unit.ToBaseFactor();
        }

        public bool Equals(QuantityLength? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Math.Abs(ToBaseUnit() - other.ToBaseUnit()) < Tolerance;
        }

        public override bool Equals(object? obj)
        {
            return obj is QuantityLength other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Math.Round(ToBaseUnit() / Tolerance).GetHashCode();
        }

        public int CompareTo(QuantityLength? other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            return ToBaseUnit().CompareTo(other.ToBaseUnit());
        }

        public static bool operator ==(QuantityLength left, QuantityLength right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        public static bool operator !=(QuantityLength left, QuantityLength right)
        {
            return !(left == right);
        }
    }
}