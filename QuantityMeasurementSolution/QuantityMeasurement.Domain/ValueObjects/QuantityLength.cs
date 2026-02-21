using System;
using QuantityMeasurement.Domain.Enums;

namespace QuantityMeasurement.Domain.ValueObjects
{
    /// <summary>
    /// Represents a generic length measurement with value and unit.
    /// Implements DRY principle by consolidating Feet and Inches logic.
    /// </summary>
    public sealed class QuantityLength : IEquatable<QuantityLength>
    {
        public double Value { get; }
        public LengthUnit Unit { get; }

        // Defining a small epsilon for floating-point comparison to handle precision issues in unit conversions.
        private const double Epsilon = 1e-6;

        /// <summary>
        /// Initializes a new instance of QuantityLength.
        /// </summary>
        public QuantityLength(double value, LengthUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Converts the quantity to base unit (Feet).
        /// </summary>
        private double ConvertToBase()
        {
            return Value * Unit.GetConversionFactor();
        }

        /// <summary>
        /// Value-based equality comparison using base conversion.
        /// </summary>
        public bool Equals(QuantityLength? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            double baseThis = Convert(Value, Unit, LengthUnit.Feet);
            double baseOther = Convert(other.Value, other.Unit, LengthUnit.Feet);

            return Math.Abs(baseThis - baseOther) < Epsilon;
        }

        public override bool Equals(object? obj)
        {
            return obj is QuantityLength other && Equals(other);
        }

        public override int GetHashCode()
        {
            return ConvertToBase().GetHashCode();
        }

        public static bool operator ==(QuantityLength? left, QuantityLength? right)
            => Equals(left, right);

        public static bool operator !=(QuantityLength? left, QuantityLength? right)
            => !Equals(left, right);

        public override string ToString()
            => $"{Value} {Unit}";

        /// <summary>
        /// Static API to convert value between units.
        /// </summary>
        public static double Convert(double value, LengthUnit source,       LengthUnit target)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            if (!Enum.IsDefined(typeof(LengthUnit), source))
                throw new ArgumentException("Invalid source unit.");

            if (!Enum.IsDefined(typeof(LengthUnit), target))
                throw new ArgumentException("Invalid target unit.");

            if (source == target)
                return value;

            double result = value * 
                (source.GetConversionFactor() / target.GetConversionFactor());

            return result;
        }

        /// <summary>
        /// Instance method conversion (returns new immutable object).
        /// </summary>
        public QuantityLength ConvertTo(LengthUnit target)
        {
            double convertedValue = Convert(Value, Unit, target);
            return new QuantityLength(convertedValue, target);
        }

        /// <summary>
        /// Adds another QuantityLength to the current instance.
        /// Result is returned in the unit of the first operand.
        /// </summary>
        /// <param name="other">The second QuantityLength.</param>
        /// <returns>New QuantityLength with summed value.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public QuantityLength Add(QuantityLength other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));
        
            if (!double.IsFinite(Value) || !double.IsFinite(other.Value))
                throw new ArgumentException("Values must be finite numbers.");
        
            // Convert both to base unit (Feet)
            double baseThis = Convert(Value, Unit, LengthUnit.Feet);
            double baseOther = Convert(other.Value, other.Unit, LengthUnit.Feet);
        
            // Add in base unit
            double baseSum = baseThis + baseOther;
        
            // Convert back to unit of first operand
            double resultValue = Convert(baseSum, LengthUnit.Feet, Unit);
        
            return new QuantityLength(resultValue, Unit);
        }
        
        /// <summary>
        /// Static overload for addition.
        /// </summary>
        public static QuantityLength Add(
            QuantityLength first,
            QuantityLength second)
        {
            if (first is null)
                throw new ArgumentNullException(nameof(first));
        
            return first.Add(second);
        }

        /// <summary>
        /// Adds two QuantityLength objects and returns result in explicitly specified target unit.
        /// </summary>
        /// <param name="first">First operand.</param>
        /// <param name="second">Second operand.</param>
        /// <param name="targetUnit">Explicit result unit.</param>
        /// <returns>New QuantityLength in target unit.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static QuantityLength Add(
            QuantityLength first,
            QuantityLength second,
            LengthUnit targetUnit)
        {
            if (first is null)
                throw new ArgumentNullException(nameof(first));

            if (second is null)
                throw new ArgumentNullException(nameof(second));

            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
                throw new ArgumentException("Invalid target unit.");

            if (!double.IsFinite(first.Value) || !double.IsFinite(second.Value))
                throw new ArgumentException("Values must be finite numbers.");

            // Convert both to base unit (Feet)
            double baseFirst = Convert(first.Value, first.Unit, LengthUnit.Feet);
            double baseSecond = Convert(second.Value, second.Unit, LengthUnit.Feet);

            // Add in base
            double baseSum = baseFirst + baseSecond;

            // Convert to explicit target unit
            double finalValue = Convert(baseSum, LengthUnit.Feet, targetUnit);

            return new QuantityLength(finalValue, targetUnit);
        }
    }
}