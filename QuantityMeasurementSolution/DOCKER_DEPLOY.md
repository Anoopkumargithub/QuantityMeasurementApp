# Render Deployment for ASP.NET WebAPI Backend

## Prerequisites

1. Docker installed locally (for testing)
2. Render account (render.com)
3. GitHub repository with your code pushed

## Local Testing with Docker

### 1. Build the Docker Image

```bash
cd QuantityMeasurementSolution
docker build -t quantity-api:latest .
```

### 2. Run Container with Environment Variables

```bash
docker run -p 8080:8080 \
  -e "ConnectionStrings__QuantityMeasurementDb=<YOUR_AZURE_SQL_CONNECTION_STRING>" \
  -e "Firebase__ProjectId=quantitymeasurementapp-6c764" \
  quantity-api:latest
```

Test it: Open `http://localhost:8080/swagger/index.html`

## Deployment to Render

### Step 1: Connect GitHub Repository

1. Go to [render.com](https://render.com)
2. Log in or sign up
3. Click **New → Web Service**
4. Select **Docker** as the runtime
5. Connect your GitHub repository
6. Select the repository

### Step 2: Configure the Service

| Setting | Value |
|---------|-------|
| **Name** | quantity-api |
| **Root Directory** | `QuantityMeasurementSolution` |
| **Runtime** | Docker |
| **Build Command** | (leave blank) |
| **Start Command** | (leave blank) |
| **Instance Type** | Free (or paid for better performance) |

### Step 3: Add Environment Variables

Click **Environment** and add these variables:

```
ConnectionStrings__QuantityMeasurementDb=Server=tcp:quantitymeasurement.database.windows.net,1433;Initial Catalog=QuantityMeasurementDB;Persist Security Info=False;User ID=<YOUR_USER>;Password=<YOUR_PASSWORD>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;

Firebase__ProjectId=quantitymeasurementapp-6c764
```

### Important Notes on Environment Variables

- Use `__` (double underscore) for nested configuration in .NET
- Example: `Firebase__ProjectId` maps to `Firebase:ProjectId` in appsettings.json
- Example: `ConnectionStrings__QuantityMeasurementDb` maps to `ConnectionStrings:QuantityMeasurementDb`

### Step 4: Deploy

1. Click **Create Web Service**
2. Render will automatically build and deploy your Docker image
3. Once deployed, you'll get a URL: `https://your-service.onrender.com`

### Step 5: Connect Frontend to Backend

In your frontend's Render environment variables, set:

```
API_BASE_URL=https://your-api.onrender.com/api/QuantityMeasurement
```

Then redeploy the frontend.

## Troubleshooting

### Check Logs

In Render dashboard, click your service and view logs to debug issues.

### Test the API

```bash
curl -X GET https://your-service.onrender.com/swagger/index.html
```

### Connection String Issues

- Verify Azure SQL Server allows Render's IP (add to firewall rules if needed)
- Test connection string locally first with Docker
- Ensure all special characters in password are properly escaped

### API Not Responding

- Check that `ASPNETCORE_URLS=http://+:8080` is set (already in Dockerfile)
- Verify health check endpoint is accessible
- Check error logs in Render dashboard

## Security Best Practices

✅ **Done in this setup:**
- Removed hardcoded credentials from committed files
- Added `.gitignore` rules for sensitive files
- Used environment variables for all secrets
- Created `.env.example` templates

✅ **For Azure SQL:**
- Ensure firewall rules allow Render IPs
- Consider using managed identities instead of SQL auth if available

✅ **For Production:**
- Don't commit real connection strings
- Use GitHub Secrets or Render's environment variable UI
- Enable HTTPS (Render does this automatically)
- Monitor logs for unauthorized access attempts

## File Changes Summary

| File | Change |
|------|--------|
| `Dockerfile` | NEW - Multi-stage build for ASP.NET Core 10 |
| `.dockerignore` | NEW - Excludes build artifacts from Docker context |
| `appsettings.json` | UPDATED - Empty credentials, uses env vars |
| `appsettings.Development.json` | UPDATED - Empty credentials for local development |
| `appsettings.Development.example.json` | NEW - Template showing expected format |
| `.gitignore` | UPDATED - Excludes `appsettings.Development.json` |

## Local Development Setup

After cloning, copy the example file and fill in your values:

```bash
cp QuantityMeasurement.Api/appsettings.Development.example.json QuantityMeasurement.Api/appsettings.Development.json
```

Then edit `appsettings.Development.json` with your local Azure SQL credentials.

## Port Information

- Local Docker: runs on port `8080`
- Render: assigns dynamic port, but exposes on `:80` (HTTPS)
- Environment variable: `ASPNETCORE_URLS=http://+:8080`
