using System;
using QuantityMeasurement.Domain.Services;
using QuantityMeasurement.Domain.ValueObjects;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Console entry point for Quantity Measurement Application.
    /// Handles only input/output. No business logic here.
    /// </summary>
    internal static class Program
    {
        private static readonly QuantityComparisonService _service = new();

        /// <summary>
        /// Application entry point.
        /// </summary>
        private static void Main()
        {
            Console.WriteLine("=== Quantity Measurement Application ===\n");

            DemonstrateFeetEquality();
            Console.WriteLine();
            DemonstrateInchesEquality();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates equality comparison for Feet values.
        /// </summary>
        private static void DemonstrateFeetEquality()
        {
            Console.WriteLine("---- FEET EQUALITY ----");

            Feet? firstFeet = ReadFeet("Enter first value in feet: ");
            if (firstFeet is null) return;

            Feet? secondFeet = ReadFeet("Enter second value in feet: ");
            if (secondFeet is null) return;

            bool result = _service.AreEqual(firstFeet, secondFeet);

            Console.WriteLine($"\nInput: {firstFeet.Value} ft and {secondFeet.Value} ft");
            Console.WriteLine($"Output: Equal ({result})");
        }

        /// <summary>
        /// Demonstrates equality comparison for Inches values.
        /// </summary>
        private static void DemonstrateInchesEquality()
        {
            Console.WriteLine("---- INCHES EQUALITY ----");

            Inches? firstInches = ReadInches("Enter first value in inches: ");
            if (firstInches is null) return;

            Inches? secondInches = ReadInches("Enter second value in inches: ");
            if (secondInches is null) return;

            bool result = _service.AreEqual(firstInches, secondInches);

            Console.WriteLine($"\nInput: {firstInches.Value} inch and {secondInches.Value} inch");
            Console.WriteLine($"Output: Equal ({result})");
        }

        /// <summary>
        /// Reads and validates Feet input from console.
        /// </summary>
        /// <param name="message">Prompt message.</param>
        /// <returns>Valid Feet object or null if invalid.</returns>
        private static Feet? ReadFeet(string message)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (!Feet.TryCreate(input, out Feet? feet))
            {
                Console.WriteLine("Invalid input. Please enter a numeric value.");
                return null;
            }

            return feet;
        }

        /// <summary>
        /// Reads and validates Inches input from console.
        /// </summary>
        /// <param name="message">Prompt message.</param>
        /// <returns>Valid Inches object or null if invalid.</returns>
        private static Inches? ReadInches(string message)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (!Inches.TryCreate(input, out Inches? inches))
            {
                Console.WriteLine("Invalid input. Please enter a numeric value.");
                return null;
            }

            return inches;
        }
    }
}