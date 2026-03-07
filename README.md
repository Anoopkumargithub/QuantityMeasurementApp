# 📏 Quantity Measurement Application

A clean, production-style .NET solution for comparing numerical quantities measured in **Feet** (UC1), built using layered architecture principles and covered with MSTest unit tests.

---

## 🚀 Project Overview

The **Quantity Measurement Application** (UC1) ensures accurate equality comparison between two numerical values measured in **feet**.

UC1 Flow:
- User inputs two values in feet
- Input is validated as numeric
- Values are compared for equality
- Result is shown to the user

---

## 🏗 Architecture
```
QuantityMeasurementSolution
│
├── QuantityMeasurementApp → Console Application (Presentation Layer)
├── QuantityMeasurement.Domain → Core Business Logic (Domain Layer)
├── QuantityMeasurement.Infrastructure → Future: ADO.NET / EF / external integrations
└── QuantityMeasurement.Tests → MSTest Unit Tests
```


---

## 📂 UC1 Implementation Notes

### Domain (Value Object)
- `Feet` is an immutable **Value Object**
- Equality uses value-based comparison (not reference-based)
- Proper `Equals`, `GetHashCode`, and operators implemented

### Domain Service
- `QuantityComparisonService` provides comparison operation
- Console app does not contain business rules

### Tests (MSTest)
UC1 tests include:
- Same value equality
- Different value inequality
- Null comparison
- Same reference (reflexive)
- Different type comparison

---

## ▶️ Run

### Run Console App
```bash
dotnet run --project QuantityMeasurementApp
```
### Run Tests
```bash
dotnet test 
```
## ✅ UC Status

-  UC1: Feet measurement equality
- [x] UC2: Inches measurement equality (separate from Feet)
    - Added `Inches` value object with value-based equality
    - Added `TryCreate(...)` numeric validation for both `Feet` and `Inches`
    - Reduced `Main()` dependency using `DemonstrateFeetEquality()` and `DemonstrateInchesEquality()`
    - Added MSTest coverage for Inches + non-numeric input validation
- [x] UC3: Generic QuantityLength class (DRY Principle)
    - Eliminated duplication between Feet and Inches
    - Introduced LengthUnit enum
    - Centralized conversion logic
    - Enabled cross-unit equality (1 ft == 12 inches)
    - Preserved UC1 and UC2 functionality
- [x] UC4: Extended Unit Support (Yards & Centimeters)
  - Added Yards and Centimeters to LengthUnit enum
  - Centralized conversion factors
  - No modification required in QuantityLength (DRY validated)
  - Full cross-unit equality support
  - Backward compatibility preserved
- [x] UC5: Unit-to-Unit Conversion
  - Added static Convert API
  - Added instance ConvertTo method
  - Implemented epsilon-based precision handling
  - Added validation for NaN/Infinity
  - Extended console app to support conversion
  - Comprehensive MSTest coverage
- [x] UC6: Addition of Two Length Units
  - Implemented Add() instance and static overload
  - Normalized to base unit (Feet) before arithmetic
  - Result returned in unit of first operand
  - Preserved immutability
  - Ensured commutativity
  - Added full MSTest coverage
- [x] UC7: Addition with Target Unit Specification
  - Added overloaded Add(first, second, targetUnit)
  - Explicit result unit support
  - Maintained backward compatibility with UC6
  - Preserved immutability and commutativity
  - Added extensive MSTest coverage
- [x] UC8: Refactored LengthUnit to standalone enum with conversion responsibility
  - Extracted conversion logic from QuantityLength
  - Implemented ConvertToBaseUnit and ConvertFromBaseUnit
  - Delegated all conversions to LengthUnit
  - Maintained backward compatibility (UC1–UC7)
  - Improved SRP and separation of concerns
- [x] UC9: Implemented QuantityWeight for weight measurements
- Introduced WeightUnit enum with conversion responsibility
- Implemented QuantityWeight value object
- Added equality with epsilon precision
- Implemented implicit and explicit addition overloads
- Ensured category type safety (Length ≠ Weight)
- Maintained immutability and SOLID compliance
- Architecture remains scalable for future categories
-[x] UC10: refactors the system into:
 - Single Generic Quantity<TUnit> class  
 - IMeasurable interface abstraction  
 - Enum-based unit behavior  
 - Full backward compatibility (UC1–UC9)  
 - Multi-category support (Length + Weight)  
 - Scalable architecture
-[x] UC11: Added Volume Measurement Support
 - Introduced VolumeUnit enum (Litre, Millilitre, Gallon)
 - Implemented base-unit conversion logic for volume
 - Enabled equality, conversion, and addition operations for volume
 - Preserved backward compatibility with UC1–UC10
 - Validated scalability of Quantity<TUnit> generic design
 - Confirmed Open-Closed Principle compliance
 - Added volume unit test coverage
- [x] UC12: Added Subtraction and Division Operations
  - Implemented `Subtract()` with implicit target unit support
  - Implemented `Subtract(other, targetUnit)` with explicit target unit support
  - Implemented `Divide()` returning a dimensionless scalar result
  - Added divide-by-zero protection and consistent validation
  - Enabled subtraction and division across Length, Weight, and Volume
  - Preserved immutability of original quantity objects
  - Added MSTest coverage for arithmetic edge cases and non-commutative behavior

---
