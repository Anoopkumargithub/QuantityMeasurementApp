using System;
using QuantityMeasurement.Domain.Enums;
using QuantityMeasurement.Domain.Services;
using QuantityMeasurement.Domain.ValueObjects;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Console entry point for Quantity Measurement Application (UC4).
    /// Supports Feet, Inches, Yards and Centimeters.
    /// </summary>
    internal static class Program
    {
        // Using a service to handle comparison logic, adhering to separation of concerns and allowing for future extensibility.
        private static readonly QuantityComparisonService _service = new();

        // Main method to run the console application.
        private static void Main()
        {
            Console.WriteLine("=== Quantity Measurement Application (UC4) ===\n");
            Console.WriteLine("Supported Units: Feet, Inches, Yards, Centimeters\n");

            QuantityLength? first = ReadQuantity("Enter first quantity");
            if (first is null) return;

            QuantityLength? second = ReadQuantity("Enter second quantity");
            if (second is null) return;

            // Compare the two quantities using the service, which abstracts the comparison logic and handles unit conversion internally.
            bool result = _service.AreEqual(first, second);

            Console.WriteLine($"\nInput: {first} and {second}");
            Console.WriteLine($"Output: Equal ({result})");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Reads a quantity from user input.
        /// </summary>
        private static QuantityLength? ReadQuantity(string label)
        {
            Console.WriteLine($"\n{label}");

            Console.Write("Enter numeric value: ");
            if (!double.TryParse(Console.ReadLine(), out double value))
            {
                Console.WriteLine("Invalid numeric value.");
                return null;
            }

            Console.Write("Enter unit (Feet/Inches/Yards/Centimeters): ");
            string? unitInput = Console.ReadLine();

            // Try parsing the unit input to the LengthUnit enum, ignoring case sensitivity. This allows for flexible user input while ensuring valid units.
            if (!Enum.TryParse(unitInput, true, out LengthUnit unit))
            {
                Console.WriteLine("Invalid unit type.");
                return null;
            }

            // Return a new QuantityLength instance with the provided value and unit. The QuantityLength class will handle
            return new QuantityLength(value, unit);
        }
    }
}