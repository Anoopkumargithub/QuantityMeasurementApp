# Quantity Measurement Application

Build, compare, convert, and calculate quantities across multiple measurement categories using a layered .NET solution.

The codebase started with the UC1 feet-equality requirement and has grown into a reusable quantity-measurement application with:

- Generic quantity modeling through `Quantity<TUnit>`
- Multiple categories: length, weight, volume, and temperature
- Console and ASP.NET Core API entry points
- Google/Firebase-based authentication for secured API access
- MSTest coverage for domain and service behavior
- Optional persistence through an in-memory repository, SQL Server via ADO.NET (console), and SQL Server via EF Core ORM (API)

## What The Application Supports

- Compare quantities across equivalent units
- Convert quantities into a target unit
- Add and subtract compatible quantities
- Divide compatible quantities to produce ratios
- Restrict unsupported arithmetic on absolute temperatures
- Persist measurement history through the repository layer

## Supported Units

- Length: Feet, Inches, Yards, Centimeters
- Weight: Gram, Kilogram, Pound, Tonne
- Volume: Litre, Millilitre, Gallon
- Temperature: Celsius, Fahrenheit, Kelvin

Temperature supports comparison and conversion. Arithmetic on temperatures is intentionally rejected by the domain rules.

## Repository Layout

The repository currently contains both the active solution projects and supporting layer projects referenced by the app and API.

```text
QuantityMeasurementSolution/
|- QuantityMeasurementSolution.slnx
|- QuantityMeasurement.Domain/            Domain units, value objects, exceptions, services
|- QuantityMeasurement.Tests/             MSTest test suite
|- QuantityMeasurementApp/                Console application
|- QuantityMeasurement.Api/               ASP.NET Core Web API
|- BusinessLayer/                         Application/service layer used by app and API
|- ModelLayer/                            DTOs, entities, and models
|- RepositoryLayer/                       In-memory and SQL Server repository implementations
`- QuantityMeasurementDb.sql              SQL Server schema setup script
```

## Runtime Behavior

- Console app: configured to use the SQL Server ADO.NET repository by default
- Web API: configured to use the SQL Server EF Core ORM repository by default
- Repository abstraction: supports saving measurements, querying history, counting records, clearing records, and releasing resources

Microservices option (new):

- `QuantityMeasurement.Microservices/QuantityMeasurement.ApiGateway`: API gateway
- `QuantityMeasurement.Microservices/QuantityMeasurement.AuthService`: auth service for JWT issuance/validation
- `QuantityMeasurement.Microservices/QuantityMeasurement.QmaService`: QMA operations + history
- `QuantityMeasurement.Microservices/docker-compose.yml`: Redis cache layer

This means the console app and API require a working SQL Server database unless you change their startup configuration.

The API also requires Firebase configuration and a valid Firebase-issued JWT for protected endpoints.

## Prerequisites

- .NET 10 SDK
- SQL Server instance if you want to run the console app with the default configuration
- Firebase project for Google Authentication (for API token validation)

The console app currently uses this local SQL Server target:

```text
Server=localhost,1433;Database=QuantityMeasurementDb;User Id=sa;Password=Admin@123;TrustServerCertificate=True;
```

If your SQL Server runs elsewhere, update the connection string in `QuantityMeasurementSolution/QuantityMeasurementApp/Program.cs` and `QuantityMeasurementSolution/QuantityMeasurement.Api/appsettings.json` before running.

For API authentication, configure the Firebase project id in:

- `QuantityMeasurementSolution/QuantityMeasurement.Api/appsettings.json`
- `QuantityMeasurementSolution/QuantityMeasurement.Api/appsettings.Development.json`

Current key:

```json
"Firebase": {
	"ProjectId": "your-firebase-project-id"
}
```

If you do not want to use SQL Server in console, set `useDatabaseRepository: false` in `QuantityMeasurementSolution/QuantityMeasurementApp/Program.cs`.

## Quick Start

Run commands from the repository root.

### Restore

```bash
dotnet restore QuantityMeasurementSolution/QuantityMeasurementSolution.slnx
```

### Run Tests

```bash
dotnet test QuantityMeasurementSolution/QuantityMeasurement.Tests/QuantityMeasurement.Tests.csproj
```

### Run The API

```bash
dotnet run --project QuantityMeasurementSolution/QuantityMeasurement.Api/QuantityMeasurement.Api.csproj
```

### Run The Microservices Stack

In separate terminals:

```bash
docker compose -f QuantityMeasurementSolution/QuantityMeasurement.Microservices/docker-compose.yml up -d
dotnet run --project QuantityMeasurementSolution/QuantityMeasurement.Microservices/QuantityMeasurement.AuthService/QuantityMeasurement.AuthService.csproj
dotnet run --project QuantityMeasurementSolution/QuantityMeasurement.Microservices/QuantityMeasurement.QmaService/QuantityMeasurement.QmaService.csproj
dotnet run --project QuantityMeasurementSolution/QuantityMeasurement.Microservices/QuantityMeasurement.ApiGateway/QuantityMeasurement.ApiGateway.csproj
```

Gateway endpoint base URLs: `http://localhost:7003/auth` and `http://localhost:7003/api/qma`

Swagger is enabled by default. After the API starts, open the reported `/swagger` URL.

All quantity API endpoints are protected and require a bearer token from Firebase Authentication:

```http
Authorization: Bearer <firebase-id-token>
```

### Run The Console App

```bash
dotnet run --project QuantityMeasurementSolution/QuantityMeasurementApp/QuantityMeasurementApp.csproj
```

By default, the console app starts with the database repository enabled.

## Database Setup

Before running the console app with the default configuration, create the database objects from the provided SQL script:

```text
QuantityMeasurementSolution/QuantityMeasurementDb.sql
```

The script creates:

- `dbo.QuantityMeasurements` for stored operation results
- `dbo.QuantityMeasurementHistory` for audit-style insert tracking
- `dbo.trg_QuantityMeasurements_InsertHistory` trigger to auto-write history rows after inserts
- supporting indexes for operation, measurement type, and creation time queries

## API Surface

The Web API exposes these endpoints under `/api/QuantityMeasurement`:

- `POST /api/QuantityMeasurement/compare`
- `POST /api/QuantityMeasurement/convert`
- `POST /api/QuantityMeasurement/add`
- `POST /api/QuantityMeasurement/subtract`
- `POST /api/QuantityMeasurement/divide`
- `GET /api/QuantityMeasurement/health`

Authentication and authorization behavior:

- JWT bearer authentication is configured against Firebase (`https://securetoken.google.com/{projectId}`)
- API applies a global authenticated-user fallback policy
- `QuantityMeasurementController` is marked with `[Authorize]`
- Requests without a valid Firebase token are rejected

Requests use transport contracts in `QuantityMeasurement.Api/Contracts`, and responses are wrapped in a standard `ApiResponse<T>` payload.

## UC18: Google Authentication and User Management

UC18 is implemented in the API by integrating Firebase token validation and enforcing authenticated access.

What is implemented:

- Google/Firebase token-based authentication via `Microsoft.AspNetCore.Authentication.JwtBearer`
- Firebase settings binding through `FirebaseOptions` (`Firebase:ProjectId` is required at startup)
- Authorization enforcement through global fallback policy and controller-level `[Authorize]`

User management model in this solution:

- User identity lifecycle (Google sign-in, account/profile management) is expected to be handled by Firebase Authentication / Google Identity
- The Quantity Measurement API validates incoming Firebase tokens and serves only authenticated users
- No separate local login/register/user CRUD endpoints are currently exposed in this API project

## Use Case Coverage

- [x] UC1: Feet equality comparison
- [x] UC2: Inches equality and numeric validation
- [x] UC3: Generic quantity model for length
- [x] UC4: Added yards and centimeters
- [x] UC5: Unit-to-unit conversion
- [x] UC6: Addition across length units
- [x] UC7: Addition with explicit target unit
- [x] UC8: Conversion responsibility moved into unit-specific logic
- [x] UC9: Added weight support
- [x] UC10: Introduced `Quantity<TUnit>`-based generic handling
- [x] UC11: Added volume support
- [x] UC12: Added subtraction and division
- [x] UC13: Centralized arithmetic logic
- [x] UC14: Added temperature with selective arithmetic restrictions
- [x] UC15: Added Web API support
- [x] UC16: Added SQL Server persistence through ADO.NET
- [x] UC17: Added ASP.NET REST integration with SQL Server EF Core ORM persistence
- [x] UC18: Added Google Authentication-based API security and authenticated user access management via Firebase

## Testing Focus

The test suite covers:

- Equality and cross-unit equivalence
- Conversion accuracy across categories
- Addition, subtraction, and explicit target-unit behavior
- Division and divide-by-zero behavior
- Temperature conversion and unsupported operations
- Service-layer orchestration behavior

Authentication and authorization are configured in the API startup pipeline and should be validated with integration/API tests using valid and invalid Firebase JWTs.
