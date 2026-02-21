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

---
