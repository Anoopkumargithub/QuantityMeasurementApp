using System;

namespace QuantityMeasurement.Domain.ValueObjects
{

    /// <summary>
    /// Represents a measurement of length, encapsulating both the value and the unit of measurement.
    /// This class is immutable and provides value-based equality and comparison based on the length measurement.
    /// It implements IEquatable for type-safe equality checks and IComparable for sorting and comparison of length measurements.
    /// The QuantityLength class uses the LengthUnit enum to specify the unit of measurement, allowing for accurate conversions and comparisons between different length units.
    /// Example usage:
    /// <code>
    /// var length1 = new QuantityLength(3, LengthUnit.Feet);
    /// var length2 = new QuantityLength(36, LengthUnit.Inches);
    /// Console.WriteLine(length1 == length2); // True (3 feet is equal to 36 inches)
    /// var length3 = new QuantityLength(5, LengthUnit.Feet);
    /// Console.WriteLine(length1.CompareTo(length3)); // -1 (length1 is less than length3)
    /// </code> 
    /// </summary>
    /// <remarks>
    /// This class is designed to be used in a domain-driven design context, where value objects represent concepts that are defined by their attributes rather than identity.
    /// The implementation ensures that two QuantityLength instances with the same measurement (regardless of the unit) are considered equal, and it provides a consistent hash code implementation based on the underlying measurement.
    /// The ToString method provides a human-readable representation of the measurement, which can be useful for debugging and logging purposes.
    /// The QuantityLength class is an integral part of the domain model for quantity measurement, as it allows for consistent handling of length measurements across the application, ensuring that all length measurements are properly represented and can be compared and converted as needed.
    /// </remarks>
    
     
    public sealed class QuantityLength 
        : IEquatable<QuantityLength>, IComparable<QuantityLength>
    {
        private const double Tolerance = 0.0001;

        public double Value { get; }
        public LengthUnit Unit { get; }

        // Constructor that initializes the QuantityLength instance with a specified value and unit of measurement.
        public QuantityLength(double value, LengthUnit unit)
        {
            if (double.IsNaN(value))
                throw new ArgumentException("Value cannot be NaN.", nameof(value));

            if (double.IsInfinity(value))
                throw new ArgumentException("Value cannot be Infinity.", nameof(value));

            Value = value;
            Unit = unit;
        }

        // Converts the current length measurement to the base unit (feet) for accurate comparisons and equality checks, allowing the QuantityLength class to handle measurements in different units seamlessly.
        private double ToBaseUnit()
        {
            return Value * Unit.ToBaseFactor();
        }

        // Implements the IEquatable interface for type-safe equality checks, allowing the QuantityLength class to determine if two length measurements are equal based on their converted values in the base unit (feet), ensuring that measurements in different units can be compared accurately.
        /// <param name="other">The other QuantityLength instance to compare with.</param>  
        /// <returns>True if the current instance is equal to the other instance; otherwise, false.</returns>
        /// <remarks>This method checks for null and reference equality first, and then compares the converted values of the two QuantityLength instances in the base unit (feet) using a tolerance to account for any potential floating-point precision issues, ensuring that measurements that are very close to each other are considered equal.</remarks>
        public bool Equals(QuantityLength? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Math.Abs(ToBaseUnit() - other.ToBaseUnit()) < Tolerance;
        }

        // Overrides the default Equals method to provide value-based equality comparison, allowing the QuantityLength class to determine if a given object is equal to the current instance based on its type and measurement value, ensuring that only objects of the same type with equivalent measurements are considered equal.
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>True if the specified object is equal to the current instance; otherwise, false.</returns>
        /// <remarks>This method checks if the provided object is a QuantityLength instance and then delegates to the type-specific Equals method for the actual equality comparison, ensuring that the equality logic is centralized in the type-specific method and that the default object equality behavior is overridden to provide value-based equality for QuantityLength instances.</remarks>
        public override bool Equals(object? obj)
        {
            return obj is QuantityLength other && Equals(other);
        }

        // Overrides GetHashCode to provide a hash code based on the underlying measurement, ensuring that equal QuantityLength instances have the same hash code, which is important for the correct behavior of hash-based collections.
        /// <returns>A hash code for the current QuantityLength instance.</returns>
        /// <remarks>This method calculates the hash code based on the converted value of the measurement in the base unit (feet) divided by a tolerance to ensure that measurements that are considered equal (i.e., within the tolerance) will produce the same hash code, which is crucial for maintaining the integrity of hash-based collections such as dictionaries and hash sets when using QuantityLength instances as keys.</remarks>
        public override int GetHashCode()
        {
            return Math.Round(ToBaseUnit() / Tolerance).GetHashCode();
        }

        // Implements the IComparable interface to allow sorting and comparison of QuantityLength instances based on their converted values in the base unit (feet), enabling the QuantityLength class to be used in sorted collections and to provide a natural ordering of length measurements regardless of their original units.
        /// <param name="other">The other QuantityLength instance to compare with.</param>
        /// <returns>A value less than zero if this instance is less than the other; zero if they are equal; a value greater than zero if this instance is greater than the other.</returns>
        /// <remarks>This method checks for null and then compares the converted values of the two QuantityLength instances in the base unit (feet) to determine their relative ordering, allowing for accurate sorting and comparison of length measurements even when they are expressed in different units.</remarks>
        public int CompareTo(QuantityLength? other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            return ToBaseUnit().CompareTo(other.ToBaseUnit());
        }

        // overloads the equality operator to provide a convenient way to compare two QuantityLength instances for equality, allowing developers to use the == operator to compare length measurements directly, which enhances readability and usability of the QuantityLength class in code.
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

        /// <summary> Converts the current QuantityLength instance to a specified target unit, allowing for flexible handling of length measurements in different units while maintaining the ability to perform accurate conversions and comparisons based on the underlying measurement logic encapsulated within the QuantityLength class. </summary>
        /// <param name="targetUnit">The target unit to which the current QuantityLength instance should be converted.</param>
        /// <returns>A new QuantityLength instance representing the converted measurement in the specified target unit.</returns>
        /// <remarks>This method checks if the provided target unit is valid and then performs the conversion by first converting the current measurement to the base unit (feet) and then converting it to the target unit using the appropriate conversion factor, ensuring that the conversion logic is centralized within the QuantityLength class and that all conversions are performed accurately based on the defined conversion factors for each length unit.</remarks>
        public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
                throw new ArgumentException("Unsupported target unit", nameof(targetUnit));

            double baseValue = ToBaseUnit(); // value in feet
            double convertedValue = baseValue / targetUnit.ToFeetFactor();

            return new QuantityLength(convertedValue, targetUnit);
        }

        /// <summary> Converts a given value from a source unit to a target unit, providing a static method for performing unit conversions without needing to create an instance of QuantityLength, which can be useful for quick conversions in various parts of the application where an instance-based approach may not be necessary. </summary>
        /// <param name="value">The value to be converted.</param>
        /// <param name="source">The source unit of the value to be converted.</param>
        /// <param name="target">The target unit to which the value should be converted.</param>
        /// <returns>The converted value in the target unit.</returns>
        /// <remarks>This method validates the input parameters to ensure that the value is finite and that the source and target units are defined in the LengthUnit enum, and then performs the conversion by first converting the value to the base unit (feet) using the source unit's conversion factor and then converting it to the target unit using the target unit's conversion factor, providing a flexible and reusable way to perform unit conversions across the application without needing to instantiate QuantityLength objects.</remarks> 
        public static double Convert(double value, LengthUnit source, LengthUnit target)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Value must be finite.", nameof(value));

            if (!Enum.IsDefined(typeof(LengthUnit), source))
                throw new ArgumentException("Unsupported source unit.", nameof(source));

            if (!Enum.IsDefined(typeof(LengthUnit), target))
                throw new ArgumentException("Unsupported target unit.", nameof(target));

            double baseValue = value * source.ToFeetFactor();
            return baseValue / target.ToFeetFactor();
        }

        public override string ToString() => $"{Value} {Unit}";
    }
}