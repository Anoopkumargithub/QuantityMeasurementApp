using System;
using QuantityMeasurement.Domain.ValueObjects;

namespace QuantityMeasurement.Domain.Services
{
    /// <summary>
    /// Service class responsible for comparing different quantity measurements, such as Feet and Inches, by utilizing their underlying QuantityLength representations for accurate comparisons.
    /// This class provides methods to compare instances of Feet, Inches, and QuantityLength for equality, ensuring that measurements in different units can be compared accurately by leveraging the conversion logic encapsulated within the QuantityLength class.
    /// Example usage:
    /// <code>
    /// var service = new QuantityComparisonService();
    /// var length1 = new Feet(3);
    /// var length2 = new Inches(36);
    /// Console.WriteLine(service.AreEqual(length1, length2)); // True (3 feet
    /// is equal to 36 inches)
    /// var length3 = new QuantityLength(5, LengthUnit.Feet);
    /// Console.WriteLine(service.AreEqual(length1, length3)); // False (3 feet is not equal to 5 feet)
    /// </code>
    /// </summary>
    /// <remarks>
    /// This service class is designed to be used in a domain-driven design context, where services encapsulate domain logic that doesn't naturally fit within entities or value objects. The QuantityComparisonService provides a clear and centralized way to compare different quantity measurements, ensuring that the comparison logic is consistent and reusable across the application. By leveraging the underlying QuantityLength representations of Feet and Inches, this service can accurately compare measurements in different units without requiring external code to handle unit conversions, thus promoting separation of concerns and enhancing the maintainability of the codebase.
    /// The methods in this service throw ArgumentNullException if either of the parameters is null,
    /// ensuring that the service is used correctly and that null values are not inadvertently compared, which could lead to unexpected behavior or errors. This design choice promotes robustness and encourages proper usage of the service methods.
    /// </remarks>
    public class QuantityComparisonService
    {
        public bool AreEqual(Feet first, Feet second)
        {
            if (first is null)
                throw new ArgumentNullException(nameof(first));

            if (second is null)
                throw new ArgumentNullException(nameof(second));

            return first.Equals(second);
        }

        // NEW METHOD FOR INCHES
        public bool AreEqual(Inches first, Inches second)
        {
            if (first is null)
                throw new ArgumentNullException(nameof(first));

            if (second is null)
                throw new ArgumentNullException(nameof(second));

            return first.Equals(second);
        }

        // NEW METHOD FOR QUANTITY LENGTH
        public bool AreEqual(QuantityLength first, QuantityLength second)
        {
            if (first is null)
                throw new ArgumentNullException(nameof(first));

            if (second is null)
                throw new ArgumentNullException(nameof(second));

            return first.Equals(second);
        }
    }
}