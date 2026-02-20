using System;

namespace QuantityMeasurement.Domain.ValueObjects
{
    public sealed class Feet : IEquatable<Feet>
    {
        public double Value { get; }

        public Feet(double value)
        {
            Value = value;
        }

        // Strongly typed equality
        public bool Equals(Feet? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Value.CompareTo(other.Value) == 0;
        }

        // Override Object.Equals
        public override bool Equals(object? obj)
        {
            if (obj is not Feet other)
                return false;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Feet? left, Feet? right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Feet? left, Feet? right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"{Value} ft";
        }
    }
}