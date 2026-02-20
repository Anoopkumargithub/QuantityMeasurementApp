using System;

namespace QuantityMeasurement.Domain.ValueObjects
{
    public sealed class Inches : IEquatable<Inches>, IComparable<Inches>
    {
        private const double Tolerance = 0.0001;

        public double Value { get; }

        public Inches(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Value must be a valid numeric measurement.", nameof(value));

            Value = value;
        }

        public bool Equals(Inches? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Math.Abs(Value - other.Value) < Tolerance;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Inches);
        }

        public override int GetHashCode()
        {
            return Math.Round(Value / Tolerance).GetHashCode();
        }

        public int CompareTo(Inches? other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (Math.Abs(Value - other.Value) < Tolerance)
                return 0;

            return Value.CompareTo(other.Value);
        }

        public static bool operator ==(Inches? left, Inches? right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Inches? left, Inches? right)
        {
            return !(left == right);
        }
    }
}