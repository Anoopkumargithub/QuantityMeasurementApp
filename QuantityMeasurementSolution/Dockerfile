# ============================================================================
# BUILD STAGE: Compile the ASP.NET application
# ============================================================================
# Use official .NET SDK 10.0 image for building (includes compiler, tools, etc.)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

# Set working directory inside container where build happens
WORKDIR /src

# Copy solution file (.slnx) - describes all projects in the solution
COPY ["QuantityMeasurementSolution.slnx", "./"]

# Copy each project file (.csproj) to preserve folder structure
# This allows Docker to cache the restore step if dependencies haven't changed
COPY ["QuantityMeasurement.Api/QuantityMeasurement.Api.csproj", "QuantityMeasurement.Api/"]
COPY ["RepositoryLayer/RepositoryLayer.csproj", "RepositoryLayer/"]
COPY ["BusinessLayer/BusinessLayer.csproj", "BusinessLayer/"]
COPY ["ModelLayer/ModelLayer.csproj", "ModelLayer/"]
COPY ["QuantityMeasurement.Domain/QuantityMeasurement.Domain.csproj", "QuantityMeasurement.Domain/"]

# Download and cache NuGet dependencies (packages from NuGet.org)
# This is a separate step so Docker caches it - faster rebuilds if code changes
RUN dotnet restore "QuantityMeasurement.Api/QuantityMeasurement.Api.csproj"

# Copy all application source code (.cs files, configuration, etc.)
COPY . .

# Compile the application in Release mode (optimized for production)
# Output goes to /app/build directory
WORKDIR "/src/QuantityMeasurement.Api"
RUN dotnet build "QuantityMeasurement.Api.csproj" -c Release -o /app/build

# ============================================================================
# PUBLISH STAGE: Create production-ready package
# ============================================================================
# Builds on the previous build stage
FROM build AS publish

# Compile and package the application for deployment (/app/publish)
# /p:UseAppHost=false means output is framework-dependent (will use runtime image)
RUN dotnet publish "QuantityMeasurement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ============================================================================
# RUNTIME STAGE: Final lightweight production image
# ============================================================================
# Use smaller runtime-only image (no compiler, build tools - just runtime)
# This is the final image Render will run
FROM mcr.microsoft.com/dotnet/aspnet:10.0

# Set working directory in production container
WORKDIR /app

# Tell Docker that our app listens on port 8080
EXPOSE 8080

# Configure ASP.NET Core to listen on port 8080 (all interfaces with +)
# Render will map this to the external port
ENV ASPNETCORE_URLS=http://+:8080

# Copy compiled and published application from the publish stage
# Only copies the necessary runtime files (much smaller than source code)
COPY --from=publish /app/publish .

# Run the application when container starts
# Executes: dotnet QuantityMeasurement.Api.dll
ENTRYPOINT ["dotnet", "QuantityMeasurement.Api.dll"]
