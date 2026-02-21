using System;
using QuantityMeasurement.Domain.ValueObjects;
using QuantityMeasurement.Domain.Services;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// The main entry point for the Quantity Measurement Application, which demonstrates the functionality of comparing different quantity measurements such as Feet and Inches using the QuantityComparisonService.
    /// This application prompts the user to input values for Feet and Inches, creates instances of the corresponding value objects, and then uses the QuantityComparisonService to compare the measurements for equality, providing a simple console-based interface for testing the domain logic implemented in the value objects and services.
    /// Example usage:
    /// <code>
    /// // User inputs:
    /// /// Enter first value in feet: 3
    /// /// Enter second value in feet: 3
    /// /// // Output:
    /// /// Input: 3 ft and 3 ft    
    /// /// /// Output: Equal (True)
    /// /// // User inputs:
    /// /// Enter first value in inches: 36
    /// /// Enter second value in inches: 36
    /// /// // Output:
    /// /// Input: 36 inch and 36 inch
    /// /// Output: Equal (True)
    /// /// </code>
    /// </summary>
    /// <remarks>
    /// This program is designed to be a simple console application that allows users to test the functionality of the quantity measurement domain model, specifically the value objects for Feet and Inches and the QuantityComparisonService. The program demonstrates how to create instances of the value objects based on user input, how to use the service to compare measurements for equality, and how to handle invalid input gracefully by prompting the user to enter valid numeric values. This application serves as a practical example of how the domain model can be used in a real-world scenario, allowing developers to see the benefits of using value objects and services in a domain-driven design context.
    /// The program is structured to be easily extendable, allowing for additional measurement types and comparison logic to be added in the future as needed, while maintaining a clear separation of concerns between the user interface (console input/output) and the domain logic (value objects and services). This design promotes maintainability and scalability of the application as it evolves over time. 
    /// </remarks>
    
     
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Quantity Measurement Application ===\n");

            DemonstrateFeetEquality();
            Console.WriteLine();
            DemonstrateInchesEquality();
        }

        // This method demonstrates the equality comparison of two Feet instances by prompting the user for input values, creating Feet objects, and using the QuantityComparisonService to determine if the measurements are equal, while also handling invalid input gracefully.
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

        // This method demonstrates the equality comparison of two Inches instances by prompting the user for input values, creating Inches objects, and using the QuantityComparisonService to determine if the measurements are equal, while also handling invalid input gracefully.
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