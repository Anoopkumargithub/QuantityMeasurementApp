# Quantity Measurement Microservices (.NET)

This folder now follows the bare-minimum architecture for transitioning QMA to microservices:

- api-gateway
- auth-service
- qma-service (includes history)
- redis (cache layer)

## Services

1. `QuantityMeasurement.ApiGateway` (port 7003)
   - YARP gateway entry point
   - Routes `/auth/*` to auth-service
   - Routes `/api/qma/*` to qma-service

2. `QuantityMeasurement.AuthService` (port 7004)
   - Issues JWT tokens with `/auth/token`
   - Validates tokens with `/auth/validate`

3. `QuantityMeasurement.QmaService` (port 7001)
   - Core QMA operations (compare, convert, add, subtract, divide)
   - Includes history endpoints:
     - `GET /api/qma/history`
     - `DELETE /api/qma/history`
   - Uses Redis as cache for history reads

4. `Redis` (port 6379)
   - Cache layer for QMA history responses
   - Started via `docker-compose.yml`

## Request Flow

Client -> API Gateway (`:7003`) -> Auth Service (`:7004`) for token

Client -> API Gateway (`:7003`) -> QMA Service (`:7001`) for business endpoints

QMA Service -> Redis (`:6379`) for cache read/write

## Run Order

From `QuantityMeasurementSolution/QuantityMeasurement.Microservices`:

```bash
docker compose up -d
```

From `QuantityMeasurementSolution` in separate terminals:

```bash
dotnet run --project QuantityMeasurement.Microservices/QuantityMeasurement.AuthService/QuantityMeasurement.AuthService.csproj
dotnet run --project QuantityMeasurement.Microservices/QuantityMeasurement.QmaService/QuantityMeasurement.QmaService.csproj
dotnet run --project QuantityMeasurement.Microservices/QuantityMeasurement.ApiGateway/QuantityMeasurement.ApiGateway.csproj
```

## API Through Gateway

- `POST http://localhost:7003/auth/token`
- `GET  http://localhost:7003/auth/validate` (Bearer token required)
- `POST http://localhost:7003/api/qma/compare` (Bearer token required)
- `POST http://localhost:7003/api/qma/convert` (Bearer token required)
- `POST http://localhost:7003/api/qma/add` (Bearer token required)
- `POST http://localhost:7003/api/qma/subtract` (Bearer token required)
- `POST http://localhost:7003/api/qma/divide` (Bearer token required)
- `GET  http://localhost:7003/api/qma/history` (Bearer token required)
- `DELETE http://localhost:7003/api/qma/history` (Bearer token required)

## Default Auth Credentials

- Username: `qma-admin`
- Password: `qma-pass`
