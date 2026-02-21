using System;
using QuantityMeasurement.Domain.ValueObjects;

namespace QuantityMeasurement.Domain.Services
{
    /// <summary>
    /// Provides comparison operations for quantity value objects.
    /// </summary>
    public sealed class QuantityComparisonService
    {
        /// <summary>
        /// Checks equality between two <see cref="Feet"/> values.
        /// </summary>
        /// <param name="first">First feet measurement.</param>
        /// <param name="second">Second feet measurement.</param>
        /// <returns>True if equal; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public bool AreEqual(Feet first, Feet second)
        {
            if (first is null) throw new ArgumentNullException(nameof(first), "First feet value cannot be null.");
            if (second is null) throw new ArgumentNullException(nameof(second), "Second feet value cannot be null.");

            return first.Equals(second);
        }
        /// <summary>
        /// Checks equality between two <see cref="Inches"/> values.
        /// </summary>
        /// <param name="first">First inches measurement.</param>
        /// <param name="second">Second inches measurement.</param>
        /// <returns>True if equal; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public bool AreEqual(Inches first, Inches second)
        {
            if (first is null) throw new ArgumentNullException(nameof(first), "First inches value cannot be null.");
            if (second is null) throw new ArgumentNullException(nameof(second), "Second inches value cannot be null.");

            return first.Equals(second);
        }
    }
}