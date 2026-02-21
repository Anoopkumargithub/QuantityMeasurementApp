using System;
using QuantityMeasurement.Domain.Enums;
using QuantityMeasurement.Domain.Services;
using QuantityMeasurement.Domain.ValueObjects;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Entry point for the Quantity Measurement Application.
    /// This application demonstrates the use of QuantityComparisonService to compare different quantity measurements (UC3).
    /// It allows users to input two measurements with their respective units and outputs whether they are equal based on value-based equality.
    /// </summary>
    internal static class Program
    {
        // Using a single instance of the service for simplicity, as it is stateless and thread-safe.
        private static readonly QuantityComparisonService _service = new();

        // Main method serves as the entry point of the application, handling user input and displaying results.
        private static void Main()
        {
            Console.WriteLine("=== Quantity Measurement Application (UC3) ===\n");

            // Read first quantity measurement from user input.
            QuantityLength? first = ReadQuantity("Enter first value: ");
            if (first is null) return;

            // Read second quantity measurement from user input.
            QuantityLength? second = ReadQuantity("Enter second value: ");
            if (second is null) return;

            // Compare the two measurements using the QuantityComparisonService and display the result. 
            bool result = _service.AreEqual(first, second);

            Console.WriteLine($"\nInput: {first} and {second}");
            Console.WriteLine($"Output: Equal ({result})");
        }

        // Helper method to read a quantity measurement from user input, including validation for numeric value and unit type.
        private static QuantityLength? ReadQuantity(string message)
        {
            Console.Write(message);
            // Validate numeric input for the quantity value, ensuring it can be parsed to a double.
            if (!double.TryParse(Console.ReadLine(), out double value))
            {
                Console.WriteLine("Invalid numeric value.");
                return null;
            }

            Console.Write("Enter unit (Feet/Inches): ");
            string? unitInput = Console.ReadLine();

            // Validate unit type input, ensuring it matches one of the defined LengthUnit enum values (case-insensitive).
            if (!Enum.TryParse(unitInput, true, out LengthUnit unit))
            {
                Console.WriteLine("Invalid unit type.");
                return null;
            }

            // Return a new QuantityLength instance based on the validated user input, which will be used for comparison in the service.
            return new QuantityLength(value, unit);
        }
    }
}