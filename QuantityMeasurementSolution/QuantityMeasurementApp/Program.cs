using System;
using QuantityMeasurement.Domain.Services;
using QuantityMeasurement.Domain.ValueObjects;

namespace QuantityMeasurementApp
{
    internal static class Program
    {
        /// <summary>
        /// Entry point for the Quantity Measurement Console Application (UC1).
        /// </summary>
        private static void Main()
        {
            Console.WriteLine("=== Quantity Measurement Application (UC1) ===\n");

            Console.Write("Enter first value in feet: ");
            if (!TryReadDouble(out double firstValue))
            {
                Console.WriteLine("Invalid input. Please enter a numeric value.");
                return;
            }

            Console.Write("Enter second value in feet: ");
            if (!TryReadDouble(out double secondValue))
            {
                Console.WriteLine("Invalid input. Please enter a numeric value.");
                return;
            }

            var firstFeet = new Feet(firstValue);
            var secondFeet = new Feet(secondValue);

            var service = new QuantityComparisonService();
            bool result = service.AreEqual(firstFeet, secondFeet);

            Console.WriteLine($"\nInput: {firstValue} ft and {secondValue} ft");
            Console.WriteLine($"Output: Equal ({result})");
        }

        /// <summary>
        /// Reads a double from console safely.
        /// </summary>
        /// <param name="value">Parsed double value.</param>
        /// <returns>True if parsing succeeded; otherwise false.</returns>
        private static bool TryReadDouble(out double value)
        {
            string? input = Console.ReadLine();
            return double.TryParse(input, out value);
        }
    }
}