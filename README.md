# VLite API - Core Connection Functions

A lean, production-grade ASP.NET Core Web API that provides HTTP wrapper functionality around VLite DLL **Core Connection Functions** with exact function name mapping.

## Overview

This API implements the **exact DLL function name mapping** approach for VLiteCommon.dll Core Connection Functions, ensuring that all endpoints, models, and methods use the precise VLite DLL function names without any translation or modification.

## Key Features

- **🔄 Exact Function Name Mapping**: All API endpoints match DLL function names precisely
- **🛡️ Thread-Safe Operations**: Proper synchronization for non-thread-safe VLite DLLs
- **🔗 Connection Management**: Production-grade connection pooling and lifecycle management
- **📝 Comprehensive Logging**: Detailed request/response logging with Serilog
- **✅ Input Validation**: FluentValidation for all request models
- **🚨 Error Handling**: Structured error responses with DLL error code mapping
- **📖 OpenAPI Documentation**: Full Swagger documentation with XML comments

## Core Connection Functions

### Implemented Endpoints

| Endpoint | HTTP Method | DLL Function | Description |
|----------|-------------|--------------|-------------|
| `/api/vcInitialise` | POST | `vcInitialise()` | Initialize VLite DLL with license |
| `/api/vcConnect` | POST | `vcConnect()` | Connect to VLite SPU device |
| `/api/vcDisconnect/{hubId}` | POST | `vcDisconnect()` | Disconnect from specific hub |
| `/api/vcTerminate` | POST | `vcTerminate()` | Terminate DLL and disconnect all |

## API Architecture

```
VLiteAPI/
├── Controllers/
│   └── VLiteCommonController.cs     # Core connection endpoints
├── Services/
│   ├── IVLiteCommonService.cs       # Service interface
│   ├── VLiteCommonService.cs        # Thread-safe DLL wrapper
│   └── ConnectionManager.cs         # Connection lifecycle management
├── Models/
│   ├── Requests/                    # Request DTOs with exact DLL names
│   ├── Responses/                   # Response DTOs with exact DLL names
│   └── Options/                     # Configuration models
├── Infrastructure/
│   ├── VLiteException.cs            # Custom exception handling
│   ├── Validators.cs                # FluentValidation rules
│   ├── Middleware.cs                # Global middleware
│   └── Extensions.cs                # DI and pipeline configuration
├── Constants.cs                     # Error codes and defaults
└── Program.cs                       # Application startup
```

## Quick Start

### 1. Build and Run

```powershell
cd VLiteAPI
dotnet build
dotnet run
```

### 2. Access Swagger UI

Navigate to: `https://localhost:7000` (Development)

### 3. Example API Calls

#### Initialize VLite DLL
```bash
POST /api/vcInitialise
{
  "licenseKey": "YOUR-LICENSE-KEY",
  "versionMajor": 1,
  "versionMinor": 0
}
```

#### Connect to SPU
```bash
POST /api/vcConnect
{
  "ipAddr": "192.168.1.100",
  "portNumber": 9221,
  "password": "admin",
  "readOnly": false
}
```

#### Disconnect from Hub
```bash
POST /api/vcDisconnect/123
```

#### Terminate DLL
```bash
POST /api/vcTerminate
```

## Configuration

### appsettings.json
```json
{
  "VLite": {
    "DefaultLicenseKey": "",
    "ConnectionTimeoutMs": 30000,
    "CommandTimeoutMs": 10000,
    "MaxConcurrentConnections": 10,
    "EnableDetailedLogging": true,
    "EnableConnectionPooling": true
  },
  "Connection": {
    "DefaultPort": 9221,
    "RetryAttempts": 3,
    "RetryDelayMs": 1000,
    "PoolSize": 5,
    "IdleTimeoutMinutes": 15
  }
}
```

## Error Handling

All API responses follow a standardized format:

### Success Response
```json
{
  "success": true,
  "data": { /* response data */ },
  "error": null,
  "timestamp": "2025-06-24T10:30:00Z"
}
```

### Error Response
```json
{
  "success": false,
  "data": null,
  "error": {
    "dllFunction": "vcConnect",
    "errorCode": -5,
    "message": "Login failed",
    "details": "Invalid credentials provided"
  },
  "timestamp": "2025-06-24T10:30:00Z"
}
```

## VLite Error Codes

| Code | Constant | Description |
|------|----------|-------------|
| 0 | `vERR_SUCCESS` | Operation successful |
| -1 | `vERR_UNDEFINED` | Undefined error |
| -2 | `vERR_DLL_VERSION` | DLL version mismatch |
| -3 | `vERR_INVALID_LICENSE` | Invalid license |
| -4 | `vERR_EXPIRED_LICENSE` | Expired license |
| -5 | `vERR_LOGIN_FAIL` | Login failed |
| -6 | `vERR_SOCKET_EXCEPTION` | Socket exception |
| -7 | `vERR_DEV_TIMEOUT` | Device timeout |
| -8 | `vERR_EXCEED_WRITE_CON` | Exceeded write connections |
| -9 | `vERR_INVALID_HUB` | Invalid hub ID |
| -10 | `vERR_DLL_UNLOAD` | DLL unload error |

## Thread Safety

The API implements proper thread safety for VLite DLL operations:

- **Semaphore-based synchronization**: Prevents concurrent DLL calls
- **Connection lifecycle management**: Tracks and manages hub connections
- **Timeout handling**: Configurable timeouts for all operations
- **Resource disposal**: Proper cleanup of resources

## Logging

Comprehensive logging using Serilog:

- **Request/Response logging**: All API calls are logged
- **DLL operation logging**: Detailed DLL function call logging
- **Error logging**: Full exception details with stack traces
- **Performance logging**: Request duration and performance metrics

Log files are written to `logs/vlite-api-{date}.txt` with daily rolling.

## Production Considerations

### Security
- Input validation on all endpoints
- Structured error responses (no sensitive data exposure)
- Configurable connection limits

### Performance
- Thread-safe operation with minimal blocking
- Connection pooling and management
- Efficient resource utilization

### Reliability
- Graceful error handling and recovery
- Proper resource disposal
- Comprehensive logging for debugging

### Scalability
- Configurable connection pools
- Asynchronous operations throughout
- Minimal memory footprint

## Development Notes

### DLL Integration
Currently, the service includes simulation methods for development purposes. In production:

1. Replace simulation methods with actual VLite DLL calls
2. Add proper DLL loading and initialization
3. Implement actual error code mapping from DLL responses

### Extending the API
To add more VLite functions:

1. Add request/response models in `Models/` folder
2. Add service methods in `IVLiteCommonService` and implementation
3. Add controller endpoints with exact DLL function names
4. Add validation rules for new request models

## Dependencies

- .NET 8.0
- ASP.NET Core 8.0
- FluentValidation.AspNetCore 11.3.0
- Serilog.AspNetCore 8.0.0
- Swashbuckle.AspNetCore 6.5.0

## License

This API wrapper is designed to work with licensed VLite DLL components. Ensure proper licensing before production use.
