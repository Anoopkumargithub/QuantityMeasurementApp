using System;
using QuantityMeasurement.Domain.ValueObjects;
using QuantityMeasurement.Domain.Services;

namespace QuantityMeasurementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Quantity Measurement Application ===");
            Console.WriteLine("Enter first value in feet:");

            if (!double.TryParse(Console.ReadLine(), out double firstInput))
            {
                Console.WriteLine("Invalid input. Please enter a numeric value.");
                return;
            }

            Console.WriteLine("Enter second value in feet:");

            if (!double.TryParse(Console.ReadLine(), out double secondInput))
            {
                Console.WriteLine("Invalid input. Please enter a numeric value.");
                return;
            }

            var firstFeet = new Feet(firstInput);
            var secondFeet = new Feet(secondInput);

            var comparisonService = new QuantityComparisonService();

            bool result = comparisonService.AreEqual(firstFeet, secondFeet);

            Console.WriteLine();
            Console.WriteLine($"Input: {firstFeet} and {secondFeet}");
            Console.WriteLine($"Output: Equal ({result})");
        }
    }
}