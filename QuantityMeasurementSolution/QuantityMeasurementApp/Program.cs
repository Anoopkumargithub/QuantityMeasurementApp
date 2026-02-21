using System;
using QuantityMeasurement.Domain.Enums;
using QuantityMeasurement.Domain.Services;
using QuantityMeasurement.Domain.ValueObjects;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Console entry point for Quantity Measurement Application (UC5).
    /// Supports Feet, Inches, Yards and Centimeters.
    /// </summary>
    internal static class Program
    {
        // Using a service to handle comparison logic, adhering to separation of concerns and allowing for future extensibility.
        private static readonly QuantityComparisonService _service = new();

        // Main method to run the console application.
        private static void Main()
        {
            Console.WriteLine("=== Quantity Measurement Application (UC5) ===\n");

            Console.WriteLine("1. Equality Check");
            Console.WriteLine("2. Unit Conversion");
            Console.Write("\nChoose option: ");

            string? choice = Console.ReadLine();

            if (choice == "1")
                DemonstrateEquality();
            else if (choice == "2")
                DemonstrateConversion();
            else
                Console.WriteLine("Invalid option.");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates equality comparison between two QuantityLength values.
        /// </summary>
        private static void DemonstrateEquality()
        {
            QuantityLength? first = ReadQuantity("Enter first quantity");
            if (first is null) return;

            QuantityLength? second = ReadQuantity("Enter second quantity");
            if (second is null) return;

            bool result = _service.AreEqual(first, second);

            Console.WriteLine($"\nInput: {first} and {second}");
            Console.WriteLine($"Output: Equal ({result})");
        }

        /// <summary>
        /// Demonstrates conversion from one unit to another.
        /// </summary>
        private static void DemonstrateConversion()
        {
            Console.Write("Enter value to convert: ");
            if (!double.TryParse(Console.ReadLine(), out double value))
            {
                Console.WriteLine("Invalid numeric value.");
                return;
            }

            Console.Write("From unit (Feet/Inches/Yards/Centimeters): ");
            if (!Enum.TryParse(Console.ReadLine(), true, out LengthUnit fromUnit))
            {
                Console.WriteLine("Invalid source unit.");
                return;
            }

            Console.Write("To unit (Feet/Inches/Yards/Centimeters): ");
            if (!Enum.TryParse(Console.ReadLine(), true, out LengthUnit toUnit))
            {
                Console.WriteLine("Invalid target unit.");
                return;
            }

            try
            {
                double result = QuantityLength.Convert(value, fromUnit, toUnit);
                Console.WriteLine($"\nConverted Result: {result}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Conversion failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Reads a quantity from user input.
        /// </summary>
        /// <param name="label">Label shown to the user.</param>
        /// <returns>QuantityLength instance if valid; otherwise null.</returns>
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

            // Try parsing the unit input to the LengthUnit enum, ignoring case sensitivity.
            if (!Enum.TryParse(unitInput, true, out LengthUnit unit))
            {
                Console.WriteLine("Invalid unit type.");
                return null;
            }

            // Return a new QuantityLength instance with the provided value and unit.
            return new QuantityLength(value, unit);
        }
    }
}