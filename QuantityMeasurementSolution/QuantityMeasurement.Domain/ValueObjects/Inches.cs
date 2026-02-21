using System;

namespace QuantityMeasurement.Domain.ValueObjects
{
    /// <summary>
    /// Represents a measurement in inches as an immutable value object.
    /// </summary>
    /// <remarks>
    /// UC2 introduces Inches equality checks (handled separately from Feet).
    /// </remarks>
    public sealed class Inches : IEquatable<Inches>
    {
        /// <summary>
        /// Gets the numeric value of the measurement in inches.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Inches"/> class.
        /// </summary>
        /// <param name="value">Numeric value in inches.</param>
        public Inches(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Tries to create an <see cref="Inches"/> instance from user input.
        /// </summary>
        /// <param name="input">Raw input string.</param>
        /// <param name="inches">The created inches value object if parsing succeeds.</param>
        /// <returns>True if numeric parsing succeeds; otherwise false.</returns>
        /// <remarks>
        /// This supports UC2 requirement to validate numeric inputs.
        /// </remarks>
        public static bool TryCreate(string? input, out Inches? inches)
        {
            inches = null;

            if (!double.TryParse(input, out double value))
                return false;

            inches = new Inches(value);
            return true;
        }

        /// <inheritdoc/>
        public bool Equals(Inches? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Value.CompareTo(other.Value) == 0;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Inches)) return false;

            return Equals((Inches)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// Equality operator for <see cref="Inches"/>.
        /// </summary>
        public static bool operator ==(Inches? left, Inches? right) => Equals(left, right);

        /// <summary>
        /// Inequality operator for <see cref="Inches"/>.
        /// </summary>
        public static bool operator !=(Inches? left, Inches? right) => !Equals(left, right);

        /// <summary>
        /// Returns a readable string representation.
        /// </summary>
        public override string ToString() => $"{Value} inch";
    }
}