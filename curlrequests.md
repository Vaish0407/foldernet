# VLite API - cURL Request Examples

This document provides comprehensive cURL examples for all VLite API endpoints. The API implements exact DLL function name mapping for VLiteCommon.dll Core Connection Functions.

## Base Configuration

- **Base URL (HTTPS)**: `https://localhost:7000`
- **Base URL (HTTP)**: `http://localhost:5000`
- **Content-Type**: `application/json`
- **API Prefix**: `/api`

## Environment Variables

For convenience, you can set these environment variables:

```bash
# For HTTPS (recommended)
export VLITE_BASE_URL="https://localhost:7000"

# For HTTP (development only)
export VLITE_BASE_URL="http://localhost:5000"
```

## Core Connection Functions

### 1. vcInitialise - Initialize VLite DLL

Initialize the VLite DLL with license validation.

**Endpoint**: `POST /api/vcInitialise`

```bash
curl -X POST "${VLITE_BASE_URL:-https://localhost:7000}/api/vcInitialise" \
  -H "Content-Type: application/json" \
  -H "Accept: application/json" \
  -d '{
    "licenseKey": "YOUR-LICENSE-KEY-HERE",
    "versionMajor": 1,
    "versionMinor": 0
  }'
```

**Example with specific license**:
```bash
curl -X POST "https://localhost:7000/api/vcInitialise" \
  -H "Content-Type: application/json" \
  -d '{
    "licenseKey": "VLITE-2024-PROD-ABC123",
    "versionMajor": 2,
    "versionMinor": 1
  }'
```

**Expected Success Response**:
```json
{
  "success": true,
  "data": {
    "success": true,
    "message": "VLite DLL initialized successfully",
    "versionInfo": "VLite v2.1.0",
    "initializedAt": "2025-06-25T10:30:00Z",
    "errorCode": 0
  },
  "error": null,
  "timestamp": "2025-06-25T10:30:00Z"
}
```

### 2. vcConnect - Connect to VLite SPU Device

Connect to a VLite SPU device and establish communication.

**Endpoint**: `POST /api/vcConnect`

```bash
curl -X POST "${VLITE_BASE_URL:-https://localhost:7000}/api/vcConnect" \
  -H "Content-Type: application/json" \
  -H "Accept: application/json" \
  -d '{
    "ipAddr": "192.168.1.100",
    "portNumber": 9221,
    "password": "admin",
    "readOnly": false
  }'
```

**Example with read-only connection**:
```bash
curl -X POST "https://localhost:7000/api/vcConnect" \
  -H "Content-Type: application/json" \
  -d '{
    "ipAddr": "10.0.0.50",
    "portNumber": 9221,
    "password": "readonly_user",
    "readOnly": true
  }'
```

**Example with custom port**:
```bash
curl -X POST "https://localhost:7000/api/vcConnect" \
  -H "Content-Type: application/json" \
  -d '{
    "ipAddr": "172.16.0.10",
    "portNumber": 8080,
    "password": "secure_password",
    "readOnly": false
  }'
```

**Expected Success Response**:
```json
{
  "success": true,
  "data": {
    "hubID": 123,
    "hubSerialNo": "SPU-001-ABC123",
    "ipAddress": "192.168.1.100",
    "port": 9221,
    "readOnly": false,
    "connectedAt": "2025-06-25T10:31:00Z",
    "connectionKey": "conn_key_abc123def456",
    "errorCode": 0
  },
  "error": null,
  "timestamp": "2025-06-25T10:31:00Z"
}
```

### 3. vcDisconnect - Disconnect from Specific Hub

Disconnect from a specific VLite hub using its Hub ID.

**Endpoint**: `POST /api/vcDisconnect/{hubId}`

```bash
curl -X POST "${VLITE_BASE_URL:-https://localhost:7000}/api/vcDisconnect/123" \
  -H "Content-Type: application/json" \
  -H "Accept: application/json"
```

**Example with different Hub ID**:
```bash
curl -X POST "https://localhost:7000/api/vcDisconnect/456" \
  -H "Content-Type: application/json"
```

**Expected Success Response**:
```json
{
  "success": true,
  "data": {
    "hubID": 123,
    "success": true,
    "message": "Hub disconnected successfully",
    "disconnectedAt": "2025-06-25T10:32:00Z",
    "errorCode": 0
  },
  "error": null,
  "timestamp": "2025-06-25T10:32:00Z"
}
```

### 4. vcTerminate - Terminate VLite DLL

Terminate the VLite DLL and disconnect all active connections.

**Endpoint**: `POST /api/vcTerminate`

```bash
curl -X POST "${VLITE_BASE_URL:-https://localhost:7000}/api/vcTerminate" \
  -H "Content-Type: application/json" \
  -H "Accept: application/json"
```

**Expected Success Response**:
```json
{
  "success": true,
  "data": {
    "success": true,
    "message": "VLite DLL terminated successfully",
    "terminatedAt": "2025-06-25T10:33:00Z",
    "errorCode": 0
  },
  "error": null,
  "timestamp": "2025-06-25T10:33:00Z"
}
```

## Hub Status and Information Functions

### 5. vcIsHubConnected - Check Hub Connection Status

Check if a specific hub is currently connected.

**Endpoint**: `GET /api/vcIsHubConnected/{hubId}`

```bash
curl -X GET "${VLITE_BASE_URL:-https://localhost:7000}/api/vcIsHubConnected/123" \
  -H "Accept: application/json"
```

**Expected Success Response**:
```json
{
  "success": true,
  "data": {
    "hubId": 123,
    "isConnected": true,
    "checkedAt": "2025-06-28T10:35:00Z",
    "errorCode": 0,
    "message": "Hub 123 connection status: Connected"
  },
  "error": null,
  "timestamp": "2025-06-28T10:35:00Z"
}
```

### 6. vcGetHubSerialFromID - Get Hub Serial Number

Retrieve the serial number for a specific hub ID.

**Endpoint**: `GET /api/vcGetHubSerialFromID/{hubId}`

```bash
curl -X GET "${VLITE_BASE_URL:-https://localhost:7000}/api/vcGetHubSerialFromID/123" \
  -H "Accept: application/json"
```

**Expected Success Response**:
```json
{
  "success": true,
  "data": {
    "hubId": 123,
    "hubSerialNo": "SPU-001-ABC123",
    "queriedAt": "2025-06-28T10:36:00Z",
    "errorCode": 0,
    "message": "Retrieved serial number for hub 123: SPU-001-ABC123"
  },
  "error": null,
  "timestamp": "2025-06-28T10:36:00Z"
}
```

### 7. vcGetHubIdFromSerial - Get Hub ID from Serial

Retrieve the hub ID for a specific serial number.

**Endpoint**: `POST /api/vcGetHubIdFromSerial`

```bash
curl -X POST "${VLITE_BASE_URL:-https://localhost:7000}/api/vcGetHubIdFromSerial" \
  -H "Content-Type: application/json" \
  -d '{
    "hubSerialNo": "SPU-001-ABC123"
  }'
```

**Expected Success Response**:
```json
{
  "success": true,
  "data": {
    "hubSerialNo": "SPU-001-ABC123",
    "hubId": 123,
    "queriedAt": "2025-06-28T10:37:00Z",
    "errorCode": 0,
    "message": "Retrieved hub ID for serial SPU-001-ABC123: 123"
  },
  "error": null,
  "timestamp": "2025-06-28T10:37:00Z"
}
```

### 8. vcGetHubIPDetails - Get Hub Network Information

Retrieve network configuration details from a hub.

**Endpoint**: `GET /api/vcGetHubIPDetails/{hubId}`

```bash
curl -X GET "${VLITE_BASE_URL:-https://localhost:7000}/api/vcGetHubIPDetails/123" \
  -H "Accept: application/json"
```

**Expected Success Response**:
```json
{
  "success": true,
  "data": {
    "hubId": 123,
    "ipAddr": "",
    "subNetMask": "255.255.255.0",
    "gateway": "192.168.1.1",
    "hostName": "SPU-001",
    "domain": "local.network",
    "dhcpStatus": true,
    "portNumber": 9221,
    "dnsServer1": "8.8.8.8",
    "dnsServer2": "8.8.4.4",
    "queriedAt": "2025-06-28T10:38:00Z",
    "errorCode": 0,
    "message": "Retrieved IP details for hub 123"
  },
  "error": null,
  "timestamp": "2025-06-28T10:38:00Z"
}
```

### 9. vcGetNTP - Get NTP Configuration

Retrieve NTP server configuration from a hub.

**Endpoint**: `GET /api/vcGetNTP/{hubId}`

```bash
curl -X GET "${VLITE_BASE_URL:-https://localhost:7000}/api/vcGetNTP/123" \
  -H "Accept: application/json"
```

**Expected Success Response**:
```json
{
  "success": true,
  "data": {
    "hubId": 123,
    "ntpServer": "pool.ntp.org",
    "ntpPollInterval": 3600,
    "queriedAt": "2025-06-28T10:39:00Z",
    "errorCode": 0,
    "message": "Retrieved NTP configuration for hub 123: pool.ntp.org"
  },
  "error": null,
  "timestamp": "2025-06-28T10:39:00Z"
}
```

### 10. vcGetHubStartUp - Get Hub Startup Time

Retrieve the last startup time of a hub.

**Endpoint**: `GET /api/vcGetHubStartUp/{hubId}`

```bash
curl -X GET "${VLITE_BASE_URL:-https://localhost:7000}/api/vcGetHubStartUp/123" \
  -H "Accept: application/json"
```

**Expected Success Response**:
```json
{
  "success": true,
  "data": {
    "hubId": 123,
    "lastStart": "2025-06-28T08:00:00Z",
    "queriedAt": "2025-06-28T10:40:00Z",
    "errorCode": 0,
    "message": "Retrieved startup time for hub 123: 2025-06-28T08:00:00Z"
  },
  "error": null,
  "timestamp": "2025-06-28T10:40:00Z"
}
```

### 11. vcGetHubTemp - Get Hub Temperature

Retrieve the current internal temperature of a hub.

**Endpoint**: `GET /api/vcGetHubTemp/{hubId}`

```bash
curl -X GET "${VLITE_BASE_URL:-https://localhost:7000}/api/vcGetHubTemp/123" \
  -H "Accept: application/json"
```

**Expected Success Response**:
```json
{
  "success": true,
  "data": {
    "hubId": 123,
    "temperatureDegC": 42,
    "queriedAt": "2025-06-28T10:41:00Z",
    "errorCode": 0,
    "message": "Retrieved temperature for hub 123: 42°C"
  },
  "error": null,
  "timestamp": "2025-06-28T10:41:00Z"
}
```

### 12. vcGetHubUtil - Get Hub Utilization

Retrieve the current percentage utilization of a hub.

**Endpoint**: `GET /api/vcGetHubUtil/{hubId}`

```bash
curl -X GET "${VLITE_BASE_URL:-https://localhost:7000}/api/vcGetHubUtil/123" \
  -H "Accept: application/json"
```

**Expected Success Response**:
```json
{
  "success": true,
  "data": {
    "hubId": 123,
    "utilization": 75,
    "queriedAt": "2025-06-28T10:42:00Z",
    "errorCode": 0,
    "message": "Retrieved utilization for hub 123: 75%"
  },
  "error": null,
  "timestamp": "2025-06-28T10:42:00Z"
}
```

### 13. vcHasHubConfigChanged - Check Configuration Changes

Check if hub configuration has changed since last check.

**Endpoint**: `GET /api/vcHasHubConfigChanged/{hubId}`

```bash
curl -X GET "${VLITE_BASE_URL:-https://localhost:7000}/api/vcHasHubConfigChanged/123" \
  -H "Accept: application/json"
```

**Expected Success Response**:
```json
{
  "success": true,
  "data": {
    "hubId": 123,
    "isConfigChanged": false,
    "queriedAt": "2025-06-28T10:43:00Z",
    "errorCode": 0,
    "message": "Hub 123 configuration status: Unchanged"
  },
  "error": null,
  "timestamp": "2025-06-28T10:43:00Z"
}
```

## Complete Workflow Example

Here's a complete workflow demonstrating the typical usage pattern:

```bash
#!/bin/bash

# Set base URL
VLITE_BASE_URL="https://localhost:7000"

echo "1. Initializing VLite DLL..."
curl -X POST "$VLITE_BASE_URL/api/vcInitialise" \
  -H "Content-Type: application/json" \
  -d '{
    "licenseKey": "YOUR-LICENSE-KEY",
    "versionMajor": 1,
    "versionMinor": 0
  }' | jq .

echo -e "\n2. Connecting to SPU device..."
CONNECT_RESPONSE=$(curl -s -X POST "$VLITE_BASE_URL/api/vcConnect" \
  -H "Content-Type: application/json" \
  -d '{
    "ipAddr": "192.168.1.100",
    "portNumber": 9221,
    "password": "admin",
    "readOnly": false
  }')

echo "$CONNECT_RESPONSE" | jq .

# Extract Hub ID from response
HUB_ID=$(echo "$CONNECT_RESPONSE" | jq -r '.data.hubID')

echo -e "\n3. Disconnecting from Hub ID: $HUB_ID..."
curl -X POST "$VLITE_BASE_URL/api/vcDisconnect/$HUB_ID" \
  -H "Content-Type: application/json" | jq .

echo -e "\n4. Terminating VLite DLL..."
curl -X POST "$VLITE_BASE_URL/api/vcTerminate" \
  -H "Content-Type: application/json" | jq .
```

## Extended Workflow Example

Here's an extended workflow including hub status monitoring:

```bash
#!/bin/bash

# Set base URL
VLITE_BASE_URL="https://localhost:7000"

echo "1. Initializing VLite DLL..."
curl -X POST "$VLITE_BASE_URL/api/vcInitialise" \
  -H "Content-Type: application/json" \
  -d '{
    "licenseKey": "YOUR-LICENSE-KEY",
    "versionMajor": 1,
    "versionMinor": 0
  }' | jq .

echo -e "\n2. Connecting to SPU device..."
CONNECT_RESPONSE=$(curl -s -X POST "$VLITE_BASE_URL/api/vcConnect" \
  -H "Content-Type: application/json" \
  -d '{
    "ipAddr": "192.168.1.100",
    "portNumber": 9221,
    "password": "admin",
    "readOnly": false
  }')

echo "$CONNECT_RESPONSE" | jq .
HUB_ID=$(echo "$CONNECT_RESPONSE" | jq -r '.data.hubID')

echo -e "\n3. Checking hub connection status..."
curl -X GET "$VLITE_BASE_URL/api/vcIsHubConnected/$HUB_ID" | jq .

echo -e "\n4. Getting hub serial number..."
curl -X GET "$VLITE_BASE_URL/api/vcGetHubSerialFromID/$HUB_ID" | jq .

echo -e "\n5. Getting hub IP details..."
curl -X GET "$VLITE_BASE_URL/api/vcGetHubIPDetails/$HUB_ID" | jq .

echo -e "\n6. Getting hub temperature..."
curl -X GET "$VLITE_BASE_URL/api/vcGetHubTemp/$HUB_ID" | jq .

echo -e "\n7. Getting hub utilization..."
curl -X GET "$VLITE_BASE_URL/api/vcGetHubUtil/$HUB_ID" | jq .

echo -e "\n8. Checking configuration changes..."
curl -X GET "$VLITE_BASE_URL/api/vcHasHubConfigChanged/$HUB_ID" | jq .

echo -e "\n9. Disconnecting from Hub ID: $HUB_ID..."
curl -X POST "$VLITE_BASE_URL/api/vcDisconnect/$HUB_ID" \
  -H "Content-Type: application/json" | jq .

echo -e "\n10. Terminating VLite DLL..."
curl -X POST "$VLITE_BASE_URL/api/vcTerminate" \
  -H "Content-Type: application/json" | jq .
```

## Error Handling Examples

### Invalid License Error
```bash
curl -X POST "https://localhost:7000/api/vcInitialise" \
  -H "Content-Type: application/json" \
  -d '{
    "licenseKey": "INVALID-LICENSE",
    "versionMajor": 1,
    "versionMinor": 0
  }'
```

**Expected Error Response**:
```json
{
  "success": false,
  "data": null,
  "error": {
    "dllFunction": "vcInitialise",
    "errorCode": -3,
    "message": "Invalid license",
    "details": "The provided license key is not valid"
  },
  "timestamp": "2025-06-25T10:30:00Z"
}
```

### Connection Timeout Error
```bash
curl -X POST "https://localhost:7000/api/vcConnect" \
  -H "Content-Type: application/json" \
  -d '{
    "ipAddr": "192.168.1.999",
    "portNumber": 9221,
    "password": "admin",
    "readOnly": false
  }'
```

**Expected Error Response**:
```json
{
  "success": false,
  "data": null,
  "error": {
    "dllFunction": "vcConnect",
    "errorCode": -7,
    "message": "Device timeout",
    "details": "Connection to device timed out"
  },
  "timestamp": "2025-06-25T10:31:00Z"
}
```

### Invalid Hub ID Error
```bash
curl -X POST "https://localhost:7000/api/vcDisconnect/999" \
  -H "Content-Type: application/json"
```

**Expected Error Response**:
```json
{
  "success": false,
  "data": null,
  "error": {
    "dllFunction": "vcDisconnect",
    "errorCode": -9,
    "message": "Invalid hub ID",
    "details": "Hub ID 999 not found or invalid"
  },
  "timestamp": "2025-06-25T10:32:00Z"
}
```

## Advanced Usage

### Using with Authentication Headers
If the API implements authentication:

```bash
curl -X POST "https://localhost:7000/api/vcInitialise" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -d '{
    "licenseKey": "YOUR-LICENSE-KEY",
    "versionMajor": 1,
    "versionMinor": 0
  }'
```

### Verbose Output for Debugging
```bash
curl -v -X POST "https://localhost:7000/api/vcInitialise" \
  -H "Content-Type: application/json" \
  -d '{
    "licenseKey": "YOUR-LICENSE-KEY",
    "versionMajor": 1,
    "versionMinor": 0
  }'
```

### Save Response to File
```bash
curl -X POST "https://localhost:7000/api/vcConnect" \
  -H "Content-Type: application/json" \
  -d '{
    "ipAddr": "192.168.1.100",
    "portNumber": 9221,
    "password": "admin",
    "readOnly": false
  }' \
  -o connect_response.json
```

### Include Response Headers
```bash
curl -i -X POST "https://localhost:7000/api/vcInitialise" \
  -H "Content-Type: application/json" \
  -d '{
    "licenseKey": "YOUR-LICENSE-KEY",
    "versionMajor": 1,
    "versionMinor": 0
  }'
```

## PowerShell Examples (Windows)

For Windows PowerShell users:

```powershell
# Initialize VLite DLL
$body = @{
    licenseKey = "YOUR-LICENSE-KEY"
    versionMajor = 1
    versionMinor = 0
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:7000/api/vcInitialise" `
    -Method POST `
    -ContentType "application/json" `
    -Body $body
```

```powershell
# Connect to SPU
$connectBody = @{
    ipAddr = "192.168.1.100"
    portNumber = 9221
    password = "admin"
    readOnly = $false
} | ConvertTo-Json

$response = Invoke-RestMethod -Uri "https://localhost:7000/api/vcConnect" `
    -Method POST `
    -ContentType "application/json" `
    -Body $connectBody

$hubId = $response.data.hubID

# Disconnect
Invoke-RestMethod -Uri "https://localhost:7000/api/vcDisconnect/$hubId" `
    -Method POST `
    -ContentType "application/json"
```

## Testing and Development

### Health Check
```bash
curl -X GET "https://localhost:7000/health" \
  -H "Accept: application/json"
```

### API Documentation
Access the Swagger UI at:
- HTTPS: https://localhost:7000/swagger
- HTTP: http://localhost:5000/swagger

### HTTPS Certificate Issues (Development)
For development with self-signed certificates, use the `-k` flag:

```bash
curl -k -X POST "https://localhost:7000/api/vcInitialise" \
  -H "Content-Type: application/json" \
  -d '{
    "licenseKey": "YOUR-LICENSE-KEY",
    "versionMajor": 1,
    "versionMinor": 0
  }'
```

## Error Codes Reference

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

## Notes

1. **HTTPS Recommended**: Use HTTPS endpoints in production
2. **License Management**: Ensure valid license keys are used
3. **Connection Limits**: Be aware of maximum concurrent connection limits
4. **Error Handling**: Always check the `success` field in responses
5. **Hub ID Management**: Store and track Hub IDs for proper disconnection
6. **Timeouts**: Default timeouts are configured but may need adjustment for your network
7. **Content-Type**: Always include `Content-Type: application/json` header for POST requests
