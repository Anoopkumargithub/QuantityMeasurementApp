# 📏 Quantity Measurement Application

A clean, production-style .NET 10 solution for comparing numerical  
quantities measured in **Feet and Inches**, built using modern architecture  
principles and fully covered with MSTest unit testing.

---

## 🚀 Project Overview

The **Quantity Measurement Application** ensures accurate equality  
comparison between two numerical values measured in:

- **Feet**
- **Inches**

This project demonstrates:

- Value Object implementation
- Proper equality contract handling
- Floating-point safe comparison (tolerance-based)
- Clean architecture separation
- Domain-driven design principles
- Full unit test coverage using MSTest
- Immutable domain modeling
- Cross-unit comparison support (UC4 Enhancement)
- Centralized comparison service with full unit test coverage (UC5)

---

## 🏗 Architecture

```
QuantityMeasurementSolution
│
├── QuantityMeasurementApp → Console Application (Presentation Layer)
├── QuantityMeasurement.Domain → Core Business Logic (Domain Layer)
└── QuantityMeasurement.Tests → MSTest Unit Tests
```


### Layer Responsibilities

### 🔹 Presentation Layer (Console App)
- Accepts user input
- Validates numeric values
- Calls domain services
- Displays formatted results
- Demonstrates cross-unit comparison (UC4)
- Contains NO business logic

### 🔹 Domain Layer
- Contains Value Objects (`Feet`, `Inches`, `QuantityLength`)
- Contains Domain Service (`QuantityComparisonService`)
- Implements equality logic
- Enforces immutability
- Performs validation
- Handles cross-unit conversion logic (UC4)
- Provides centralized comparison methods with null checks (UC5)

### 🔹 Test Layer
- Provides full MSTest coverage
- Validates equality contracts
- Ensures null and edge-case handling
- Protects backward compatibility
- Validates cross-unit behavior (UC4)
- Validates QuantityComparisonService behavior (UC5)

---

## 📦 Value Objects

Location:  
`QuantityMeasurement.Domain/ValueObjects`

### 1️⃣ Feet

Features:
- Implements `IEquatable<Feet>`
- Implements `IComparable<Feet>`
- Overrides `Equals()` and `GetHashCode()`
- Supports `==` and `!=`
- Uses tolerance-based floating comparison
- Fully immutable
- Validates against NaN and Infinity

---

### 2️⃣ Inches (UC2 Addition)

Features:
- Implements `IEquatable<Inches>`
- Implements `IComparable<Inches>`
- Overrides `Equals()` and `GetHashCode()`
- Supports `==` and `!=`
- Uses tolerance-based floating comparison
- Fully immutable
- Validates against NaN and Infinity
- Maintains value-based equality

> Note: Feet and Inches are treated as separate value objects and are not compared against each other.

---

### 3️⃣ QuantityLength (UC3 – Generic Quantity Implementation)

Represents any length measurement using:

- `double Value`
- `LengthUnit Unit`

Features:

- Eliminates duplication between Feet and Inches
- Implements `IEquatable<QuantityLength>`
- Implements `IComparable<QuantityLength>`
- Overrides `Equals()` and `GetHashCode()`
- Supports `==` and `!=`
- Converts all values to a common base unit (Feet)
- Supports cross-unit comparison (e.g., 1 Foot == 12 Inches)
- Uses tolerance-based floating comparison
- Fully immutable
- Validates against NaN and Infinity
- Maintains full equality contract compliance

---

### 4️⃣ LengthUnit Enum (UC3)

Defines supported measurement units:

- Feet
- Inches

Features:

- Type-safe unit handling
- Centralized conversion factors
- Eliminates magic numbers
- Enables scalable addition of new units

---

### 5️⃣ UC4 – Cross Unit Equality Enhancement

UC4 enables full comparison between different measurement units.

Example:
- 1 Foot == 12 Inches
- 2 Feet == 24 Inches
- 0.5 Foot == 6 Inches

Enhancements:

- All units are converted internally to a base unit (Feet)
- Equality logic works seamlessly across units
- No duplication of conversion logic
- Fully backward compatible with UC1, UC2, and UC3
- Maintains strict tolerance-based floating comparison
- Preserves equality contract compliance
- No breaking changes introduced

---

### 6️⃣ UC5 – QuantityComparisonService Enhancement

Centralized domain service to compare measurements.

---

## 🔧 Domain Service — QuantityComparisonService

Location:  
`QuantityMeasurement.Domain/Services`

Responsibilities:

- Compares two `Feet` objects
- Compares two `Inches` objects
- Compares two `QuantityLength` objects
- Validates null inputs
- Supports cross-unit comparisons
- Provides centralized comparison logic
- Preserves equality contract
- Maintains backward compatibility

Methods:

```
AreEqual(Feet first, Feet second)
AreEqual(Inches first, Inches second)
AreEqual(QuantityLength first, QuantityLength second)
```


Features:

- Throws `ArgumentNullException` for null parameters
- Handles tolerance-based equality internally
- Supports cross-unit comparisons transparently
- Fully tested with MSTest (UC5 Tests)

Example:
```csharp
AreEqual(1 Foot, 12 Inches) → True
AreEqual(2 Feet, 24 Inches) → True
AreEqual(1 Foot, 10 Inches) → False
```


---

## 🖥 Console Application Flow

The application now demonstrates:

- Feet equality check
- Inches equality check
- Cross-unit equality check (UC4)
- Service-based comparisons (UC5)

Example:
=== Quantity Measurement Application ===

Input: 1.0 ft and 1.0 ft
Output: Equal (True)

Input: 1.0 inch and 1.0 inch
Output: Equal (True)

Input: 1.0 ft and 12.0 inch
Output: Equal (True)

---

## 🧪 MSTest Unit Tests

Location:  
`QuantityMeasurement.Tests`

### Test Coverage Includes:

- Reflexive equality
- Symmetric equality
- Transitive equality
- Same reference comparison
- Different value comparison
- Null comparison handling
- Exception testing (NaN / Infinity)
- Operator overload validation
- HashCode consistency validation
- Tolerance-based floating comparison
- Cross-unit equality validation (UC3 & UC4)
- QuantityComparisonService tests (UC5)
- CompareTo behavior validation
- Backward compatibility verification

All UC1 through UC5 features include full test coverage to ensure production-grade reliability.

---

## ⚠ UC5 Enhancement Summary

UC5 improves the system by:

- Providing a centralized comparison service
- Supporting Feet, Inches, and QuantityLength objects
- Enabling cross-unit comparisons transparently
- Handling null and invalid inputs safely
- Maintaining strict equality contracts
- Supporting tolerance-based comparisons
- Fully covered with unit tests for reliability

---

## 🧪 Running the Project

### Build
```
dotnet build
```


### Run Console Application
```
dotnet run --project QuantityMeasurementApp
```

### Run Unit Tests
```
dotnet test
```

---

## 🛠 Tech Stack

- .NET 10
- C#
- MSTest
- Clean Architecture
- Domain-Driven Design principles

---

## 🎯 Learning Outcomes

- Designing scalable measurement systems
- Implementing cross-unit conversion safely
- Maintaining backward compatibility while extending features
- Writing conversion-aware equality logic
- Architecting extensible domain models
- Centralizing comparison logic for production-grade reliability
