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

---

## 🏗 Architecture

```
QuantityMeasurementSolution
│
├── QuantityMeasurementApp      → Console Application (Presentation Layer)
├── QuantityMeasurement.Domain  → Core Business Logic (Domain Layer)
└── QuantityMeasurement.Tests   → MSTest Unit Tests
```

### Layer Responsibilities

### 🔹 Presentation Layer (Console App)
- Accepts user input
- Validates numeric values
- Calls domain services
- Displays formatted results
- Contains NO business logic

### 🔹 Domain Layer
- Contains Value Objects (`Feet`, `Inches`)
- Contains Domain Service (`QuantityComparisonService`)
- Implements equality logic
- Enforces immutability
- Performs validation

### 🔹 Test Layer
- Provides full MSTest coverage
- Validates equality contracts
- Ensures null and edge-case handling
- Protects backward compatibility

---

## 🧠 Design Principles Applied

- Encapsulation  
- Immutability  
- Separation of Concerns  
- Single Responsibility Principle  
- Equality Contract Compliance  
- Null Safety & Type Safety  
- Floating-Point Safe Comparison  
- Clean Architecture  

---

## 📂 Key Components

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

## 🔧 Domain Service — QuantityComparisonService

Location:  
`QuantityMeasurement.Domain/Services`

Responsibilities:

- Compares two `Feet` objects
- Compares two `Inches` objects
- Compares two `QuantityLength` objects (UC3)
- Validates null inputs
- Centralizes comparison logic
- Preserves equality contract
- Maintains backward compatibility

Methods:

```
AreEqual(Feet first, Feet second)
AreEqual(Inches first, Inches second)
AreEqual(QuantityLength first, QuantityLength second)
```

---

## 🖥 Console Application Flow

The application demonstrates:

- Feet equality check
- Inches equality check

Example:

```
=== Quantity Measurement Application ===

Input: 1.0 ft and 1.0 ft
Output: Equal (True)

Input: 1.0 inch and 1.0 inch
Output: Equal (True)
```

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
- Cross-unit equality validation (UC3)
- CompareTo behavior validation
- Backward compatibility verification

All UC1, UC2, and UC3 features include full test coverage to ensure production-grade reliability.

---

## ⚠ Previous Limitation (Resolved in UC3)

Feet and Inches previously contained similar logic, introducing duplication.

UC3 refactored the implementation to:

- Introduce a generic `QuantityLength` class
- Centralize conversion logic
- Eliminate duplication
- Improve scalability
- Preserve backward compatibility


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

- Designing immutable value objects
- Implementing correct equality patterns
- Handling floating-point precision safely
- Writing clean, testable domain logic
- Structuring a production-ready .NET solution
- Writing meaningful, contract-validating unit tests
- Maintaining architectural discipline while extending features

---