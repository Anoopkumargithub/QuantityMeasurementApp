using System;

namespace QuantityMeasurement.Domain.ValueObjects
{

    /// <summary>
    /// Represents a measurement in inches.
    /// This class is immutable and provides value-based equality.
    /// It implements IEquatable for type-safe equality checks and IComparable for sorting.
    /// The internal representation uses a QuantityLength to handle unit conversions and comparisons.
    /// Example usage:
    /// <code>
    /// var length1 = new Inches(12);
    /// var length2 = new Inches(12);
    /// Console.WriteLine(length1 == length2); // True
    /// var length3 = new Inches(24);
    /// Console.WriteLine(length1.CompareTo(length3)); // -1 (length1 is less than length3)
    /// </code>
    /// </summary>
    /// <remarks>
    /// This class is designed to be used in a domain-driven design context, where value objects represent concepts that are defined by their attributes rather than identity.
    /// The internal QuantityLength class is responsible for handling the actual measurement logic, including unit conversions and comparisons, allowing the Inches class to focus on representing the concept of inches as a value object.
    /// The implementation ensures that two Inches instances with the same value are considered equal, and it provides a consistent hash code implementation based on the underlying quantity.
    /// The ToString method provides a human-readable representation of the measurement, which can be useful for debugging and logging purposes.
    ///    /// </remarks>
    

    public sealed class Inches : IEquatable<Inches>, IComparable<Inches>
    {
        private readonly QuantityLength _quantity;

        public double Value => _quantity.Value;

        // Constructor that initializes the Inches instance with a specified value in inches.
        public Inches(double value)
        {
            _quantity = new QuantityLength(value, LengthUnit.Inches);
        }

        // Implements the IEquatable interface for type-safe equality checks.
        // <param name="other">The other Inches instance to compare with.</param>
        // <returns>True if the current instance is equal to the other instance; otherwise, false.</returns>
        // <remarks>This method checks for null and then compares the underlying quantity of the two Inches instances to determine equality.</remarks>
        public bool Equals(Inches? other)
        {
            if (other is null)
                return false;

            return _quantity.Equals(other._quantity);
        }

        // Overrides the default Equals method to provide value-based equality comparison.
        // <param name="obj">The object to compare with the current instance.</param>
        // <returns>True if the specified object is equal to the current instance; otherwise, false.</returns>
        // <remarks>This method checks if the provided object is an Inches instance and then delegates to the type-specific Equals method for the actual equality comparison.</remarks>
        public override bool Equals(object? obj)
        {
            return obj is Inches other && Equals(other);
        }

        // Overrides GetHashCode to provide a hash code based on the underlying quantity, ensuring that equal Inches instances have the same hash code.
        // <returns>A hash code for the current Inches instance.</returns>
        public override int GetHashCode()
        {
            return _quantity.GetHashCode();
        }

        // Implements the IComparable interface to allow sorting and comparison of Inches instances based on their quantity.
        // <param name="other">The other Inches instance to compare with.</param>
        // <returns>A value less than zero if this instance is less than the other; zero if they are equal; a value greater than zero if this instance is greater than the other.</returns>
        public int CompareTo(Inches? other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            return _quantity.CompareTo(other._quantity);
        }

        // overloads the equality operator to provide a convenient way to compare two Inches instances for equality.
        // <param name="left">The left Inches instance to compare.</param>
        // <param name="right">The right Inches instance to compare.</param>
        // <returns>True if the two instances are equal; otherwise, false.</returns>
        public static bool operator ==(Inches left, Inches right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        // overloads the inequality operator to provide a convenient way to compare two Inches instances for inequality.
        // <param name="left">The left Inches instance to compare.</param>
        // <param name="right">The right Inches instance to compare.</param>
        // <returns>True if the two instances are not equal; otherwise, false.</returns>
        public static bool operator !=(Inches left, Inches right)
        {
            return !(left == right);
        }
    }
}