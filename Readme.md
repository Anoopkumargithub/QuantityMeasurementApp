# 📏 Quantity Measurement Application

A clean, production-style .NET solution for comparing numerical
quantities measured in **Feet**, built using modern architecture
principles and fully covered with MSTest unit testing.

------------------------------------------------------------------------

## 🚀 Project Overview

The Quantity Measurement Application ensures accurate equality
comparison between two numerical values measured in feet.

This project demonstrates:

-   Value Object implementation
-   Proper equality contract handling
-   Floating-point safe comparison
-   Clean architecture separation
-   Domain-driven design principles
-   Full unit test coverage using MSTest

------------------------------------------------------------------------

## 🏗 Architecture

QuantityMeasurementSolution

├── QuantityMeasurementApp → Console Application (Presentation Layer)\
├── QuantityMeasurement.Domain → Core Business Logic (Domain Layer)\
└── QuantityMeasurement.Tests → MSTest Unit Tests

------------------------------------------------------------------------

## 🧠 Design Principles Applied

-   Encapsulation\
-   Immutability\
-   Separation of Concerns\
-   Single Responsibility Principle\
-   Equality Contract Compliance\
-   Null Safety & Type Safety

------------------------------------------------------------------------

## 📂 Key Components

### Value Object -- Feet

Location: QuantityMeasurement.Domain/ValueObjects

Features: - Implements IEquatable`<Feet>`{=html} - Overrides Equals()
and GetHashCode() - Supports == and != operators - Uses CompareTo() for
floating-point comparison - Fully immutable

------------------------------------------------------------------------

### Domain Service -- QuantityComparisonService

Location: QuantityMeasurement.Domain/Services

Responsibilities: - Compares two Feet objects - Validates null inputs -
Centralizes comparison logic

------------------------------------------------------------------------

### Console Application

Location: QuantityMeasurementApp

Responsibilities: - Accepts user input - Validates numeric values -
Calls domain service - Displays result

------------------------------------------------------------------------

### MSTest Unit Tests

Location: QuantityMeasurement.Tests

Test Coverage: - Reflexive equality - Symmetric equality - Transitive
behavior - Null handling - Different value comparison - Operator
overload validation - Exception handling

------------------------------------------------------------------------

## 🧪 Running the Project

Build: dotnet build

Run: dotnet run --project QuantityMeasurementApp

Test: dotnet test

------------------------------------------------------------------------

## 🖥 Example Output

=== Quantity Measurement Application === Enter first value in feet: 1.0
Enter second value in feet: 1.0

Input: 1 ft and 1 ft\
Output: Equal (True)

------------------------------------------------------------------------

## 🛠 Tech Stack

-   .NET 10
-   C#
-   MSTest
-   Clean Architecture Principles

------------------------------------------------------------------------

## 🎯 Learning Outcomes

-   Designing immutable value objects
-   Implementing correct equality patterns
-   Writing clean, testable domain logic
-   Structuring a production-ready .NET solution
-   Writing meaningful unit tests

------------------------------------------------------------------------

Built as part of professional backend architecture practice.
