using System;

namespace QuantityMeasurement.Domain.ValueObjects
{
    /// <summary>
    /// Represents a measurement in feet as an immutable value object.
    /// </summary>
    /// <remarks>
    /// UC1 focuses on value-based equality for feet measurements.
    /// </remarks>
    public sealed class Feet : IEquatable<Feet>
    {
        /// <summary>
        /// Gets the numeric value of the measurement in feet.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Feet"/> class.
        /// </summary>
        /// <param name="value">Numeric value in feet.</param>
        public Feet(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Feet"/> is equal to the current <see cref="Feet"/>.
        /// </summary>
        /// <param name="other">The other feet object to compare.</param>
        /// <returns>True if both have the same value; otherwise false.</returns>
        public bool Equals(Feet? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            // Use CompareTo instead of == to align with UC1 "Double.compare" intent.
            return Value.CompareTo(other.Value) == 0;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="Feet"/>.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the object is a <see cref="Feet"/> and values match; otherwise false.</returns>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Feet)) return false;

            return Equals((Feet)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Equality operator for <see cref="Feet"/>.
        /// </summary>
        public static bool operator ==(Feet? left, Feet? right) => Equals(left, right);

        /// <summary>
        /// Inequality operator for <see cref="Feet"/>.
        /// </summary>
        public static bool operator !=(Feet? left, Feet? right) => !Equals(left, right);

        /// <summary>
        /// Returns a readable string representation.
        /// </summary>
        /// <returns>Formatted string in feet.</returns>
        public override string ToString() => $"{Value} ft";

        /// <summary>
        /// Tries to create a <see cref="Feet"/> instance from user input.
        /// </summary>
        /// <param name="input">Raw input string.</param>
        /// <param name="feet">The created feet value object if parsing         succeeds.</param>
        /// <>True if numeric parsing succeeds; otherwise false.</      returns>
        /// <remarks>
        /// This supports UC2 requirement to validate numeric inputs.
        /// </remarks>
        public static bool TryCreate(string? input, out Feet? feet)
        {
            feet = null;
        
            if (!double.TryParse(input, out double value))
                return false;
        
            feet = new Feet(value);
            return true;
        }
    }
}