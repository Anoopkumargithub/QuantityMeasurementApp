using System;

namespace QuantityMeasurement.Domain.ValueObjects
{

    /// <summary>
    /// Represents a measurement in feet.
    /// This class is immutable and provides value-based equality.
    /// It implements IEquatable for type-safe equality checks and IComparable for sorting.
    /// The internal representation uses a QuantityLength to handle unit conversions and comparisons.
    /// Example usage:
    /// <code>
    /// var length1 = new Feet(3);
    /// var length2 = new Feet(3);
    /// Console.WriteLine(length1 == length2); // True
    /// var length3 = new Feet(5);
    /// Console.WriteLine(length1.CompareTo(length3)); // -1 (length1 is less than length3)
    /// </code>
    /// </summary>
    /// <remarks>
    /// This class is designed to be used in a domain-driven design context, where value objects represent concepts that are defined by their attributes rather than identity.
    /// The internal QuantityLength class is responsible for handling the actual measurement logic, including unit conversions and comparisons, allowing the Feet class to focus on representing the concept of feet as a value object.
    /// The implementation ensures that two Feet instances with the same value are considered equal, and it provides a consistent hash code implementation based on the underlying quantity.
    /// The ToString method provides a human-readable representation of the measurement, which can be useful for debugging and logging purposes.
    ///     </remarks>
    /// <seealso cref="QuantityLength"/>
     
    
    public sealed class Feet : IEquatable<Feet>, IComparable<Feet>
    {
        // The internal quantity representation in feet, used for equality and comparison operations.
        private readonly QuantityLength _quantity;

        // Public property to expose the value of the measurement in feet, allowing external code to access the measurement value while keeping the internal representation encapsulated.
        public double Value => _quantity.Value;

        // Constructor that initializes the Feet instance with a specified value in feet.
        public Feet(double value)
        {
            _quantity = new QuantityLength(value, LengthUnit.Feet);
        }

        // Implements the IEquatable interface for type-safe equality checks.
        /// <param name="other">The other Feet instance to compare with.</param>
        /// <returns>True if the current instance is equal to the other instance; otherwise, false.</returns>
        /// <remarks>This method checks for null and then compares the underlying quantity of the two Feet instances to determine equality.</remarks>
        public bool Equals(Feet? other)
        {
            if (other is null)
                return false;

            return _quantity.Equals(other._quantity);
        }

        // Overrides the default Equals method to provide value-based equality comparison. 
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>True if the specified object is equal to the current instance; otherwise, false.</returns>
        /// <remarks>This method checks if the provided object is a Feet instance and then delegates to the type-specific Equals method for the actual equality comparison.</remarks>
        public override bool Equals(object? obj)
        {
            return obj is Feet other && Equals(other);
        }

        // Overrides GetHashCode to provide a hash code based on the underlying quantity, ensuring that equal Feet instances have the same hash code.
        // <returns>A hash code for the current object.</returns>
        /// <remarks>This method returns the hash code of the underlying quantity, which ensures that two Feet instances that are considered equal (i.e., have the same quantity) will produce the same hash code, which is important for the correct behavior of hash-based collections.</remarks>
        public override int GetHashCode()
        {
            return _quantity.GetHashCode();
        }

        // Implements the IComparable interface to allow sorting and comparison of Feet instances based on their quantity.
        // <param name="other">The other Feet instance to compare with.</param>
        // <returns>A value less than zero if the current instance is less than the other; zero if they are equal; a value greater than zero if the current instance is greater than the other.</returns>
        /// <remarks>This method checks for null and then compares the underlying quantity of the two Feet instances to determine their relative ordering.</remarks>
        public int CompareTo(Feet? other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            return _quantity.CompareTo(other._quantity);
        }

        // overloads the equality operator to provide a convenient way to compare two Feet instances for equality.
        public static bool operator ==(Feet left, Feet right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        // overloads the inequality operator to provide a convenient way to compare two Feet instances for inequality.
        public static bool operator !=(Feet left, Feet right)
        {
            return !(left == right);
        }

        // overloads the less than operator to allow comparison of two Feet instances, returning true if the left instance is less than the right instance.
        public override string ToString()
        {
            return $"{Value} ft";
        }
    }
}