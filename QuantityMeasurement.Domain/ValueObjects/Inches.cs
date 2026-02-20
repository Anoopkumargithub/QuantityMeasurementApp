using System;

namespace QuantityMeasurement.Domain.ValueObjects
{
    public sealed class Inches : IEquatable<Inches>, IComparable<Inches>
    {
        private readonly QuantityLength _quantity;

        public double Value => _quantity.Value;

        public Inches(double value)
        {
            _quantity = new QuantityLength(value, LengthUnit.Inches);
        }

        public bool Equals(Inches? other)
        {
            if (other is null)
                return false;

            return _quantity.Equals(other._quantity);
        }

        public override bool Equals(object? obj)
        {
            return obj is Inches other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _quantity.GetHashCode();
        }

        public int CompareTo(Inches? other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            return _quantity.CompareTo(other._quantity);
        }

        public static bool operator ==(Inches left, Inches right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        public static bool operator !=(Inches left, Inches right)
        {
            return !(left == right);
        }
    }
}