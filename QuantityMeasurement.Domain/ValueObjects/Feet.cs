using System;

namespace QuantityMeasurement.Domain.ValueObjects
{
    public sealed class Feet : IEquatable<Feet>, IComparable<Feet>
    {
        private readonly QuantityLength _quantity;

        public double Value => _quantity.Value;

        public Feet(double value)
        {
            _quantity = new QuantityLength(value, LengthUnit.Feet);
        }

        public bool Equals(Feet? other)
        {
            if (other is null)
                return false;

            return _quantity.Equals(other._quantity);
        }

        public override bool Equals(object? obj)
        {
            return obj is Feet other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _quantity.GetHashCode();
        }

        public int CompareTo(Feet? other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            return _quantity.CompareTo(other._quantity);
        }

        public static bool operator ==(Feet left, Feet right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        public static bool operator !=(Feet left, Feet right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"{Value} ft";
        }
    }
}