using System;
using QuantityMeasurement.Domain.ValueObjects;
using QuantityMeasurement.Domain.Services;

namespace QuantityMeasurementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Quantity Measurement Application ===\n");

            DemonstrateFeetEquality();
            Console.WriteLine();
            DemonstrateInchesEquality();
        }

        private static void DemonstrateFeetEquality()
        {
            Console.WriteLine("Enter first value in feet:");

            if (!double.TryParse(Console.ReadLine(), out double firstValue))
            {
                Console.WriteLine("Invalid input. Please enter a numeric value.");
                return;
            }

            Console.WriteLine("Enter second value in feet:");

            if (!double.TryParse(Console.ReadLine(), out double secondValue))
            {
                Console.WriteLine("Invalid input. Please enter a numeric value.");
                return;
            }

            var firstFeet = new Feet(firstValue);
            var secondFeet = new Feet(secondValue);

            var service = new QuantityComparisonService();
            bool result = service.AreEqual(firstFeet, secondFeet);

            Console.WriteLine($"Input: {firstValue} ft and {secondValue} ft");
            Console.WriteLine($"Output: Equal ({result})");
        }

        private static void DemonstrateInchesEquality()
        {
            Console.WriteLine("Enter first value in inches:");

            if (!double.TryParse(Console.ReadLine(), out double firstValue))
            {
                Console.WriteLine("Invalid input. Please enter a numeric value.");
                return;
            }

            Console.WriteLine("Enter second value in inches:");

            if (!double.TryParse(Console.ReadLine(), out double secondValue))
            {
                Console.WriteLine("Invalid input. Please enter a numeric value.");
                return;
            }

            var firstInches = new Inches(firstValue);
            var secondInches = new Inches(secondValue);

            var service = new QuantityComparisonService();
            bool result = service.AreEqual(firstInches, secondInches);

            Console.WriteLine($"Input: {firstValue} inch and {secondValue} inch");
            Console.WriteLine($"Output: Equal ({result})");
        }
    }
}