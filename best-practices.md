# VLite API Best Practices

## Overview

Coding standards and response formats for VLite API - a wrapper for VLiteCommon.dll, VLiteConfig.dll, and VLiteHub.dll.

## Response Standards

### Standard API Format

```json
{
  "success": boolean,
  "data": object|null,
  "error": {
    "dllFunction": string,
    "errorCode": number,
    "message": string,
    "details": string
  }|null,
  "timestamp": string
}
```

### Raw API Format

Returns unmodified DLL response (integer) for maximum compatibility.

## API Classifications

| Type | Endpoints | Format |
|------|-----------|--------|
| **Standard** | vcInitialise, vcConnect, vcTerminate, VLiteConfig/Hub | Wrapped response |
| **Raw** | vcDisconnect | Raw integer |

## Models

```csharp
public class VLiteApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public VLiteError? Error { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

public class VLiteError
{
    public string DLLFunction { get; set; }
    public int ErrorCode { get; set; }
    public string Message { get; set; }
    public string? Details { get; set; }
}
```

## HTTP Status Mapping

| Code | Usage | VLite Errors |
|------|-------|--------------|
| 200 | Success | errorCode = 0 |
| 400 | Invalid params/general errors | Most DLL errors |
| 401 | Authentication | vERR_LOGIN_FAIL, license |
| 403 | Read-only violations | vERR_READONLY_CON |
| 404 | Invalid IDs | vERR_INVALID_HUB/DAU |
| 408 | Timeout | vERR_DEV_TIMEOUT |
| 409 | Conflicts | vERR_VG_EXISTS |
| 499 | Cancelled | OperationCanceledException |
| 500 | Server errors | Unhandled exceptions |

## Controller Implementation

### Standard Endpoint

```csharp
[HttpPost(Constants.Routes.vcConnect)]
public async Task<IActionResult> vcConnect([FromBody] vcConnectRequest request, CancellationToken cancellationToken = default)
{
    try
    {
        var result = await _service.vcConnectAsync(request, cancellationToken);
        return Ok(VLiteApiResponse<vcConnectResponse>.CreateSuccess(result));
    }
    catch (VLiteException ex) when (ex.ErrorCode == Constants.ErrorCodes.vERR_INVALID_HUB)
    {
        return NotFound(VLiteApiResponse<object>.CreateError(ex.ToVLiteError()));
    }
    catch (VLiteException ex)
    {
        return BadRequest(VLiteApiResponse<object>.CreateError(ex.ToVLiteError()));
    }
    catch (OperationCanceledException)
    {
        return StatusCode(499, /* cancelled error response */);
    }
    catch (Exception ex)
    {
        return StatusCode(500, /* server error response */);
    }
}
```

### Raw Endpoint

```csharp
[HttpPost(Constants.Routes.vcDisconnect)]
public async Task<IActionResult> vcDisconnect(int hubId, CancellationToken cancellationToken = default)
{
    try
    {
        var result = await _service.vcDisconnectAsync(hubId, cancellationToken);
        return Ok(result); // Raw integer response
    }
    catch (Exception ex)
    {
        return BadRequest(VLiteApiResponse<object>.CreateError(/* error */));
    }
}
```

## Service Layer

- **Thread Safety**: Use `SemaphoreSlim` for DLL synchronization
- **Raw Methods**: Return DLL response as-is
- **Standard Methods**: Enhance with metadata (timestamps, connection keys, etc.)

## Logging

```csharp
// Success
_logger.LogInformation("vcConnect completed, HubID: {HubID}", hubId);

// Error
_logger.LogError("vcConnect failed, ErrorCode: {ErrorCode}", result);

// Exception
_logger.LogError(ex, "Unexpected error in vcConnect");
```

## Documentation Requirements

- XML comments for all public methods
- Swagger documentation with examples
- Document error codes and meanings

## Testing

- Test success/error response formats
- Validate timestamp and metadata
- Test raw endpoints return integers
- Cover all error scenarios

## Security

- Validate all inputs with data annotations
- Sanitize string parameters
- Log detailed errors internally, return sanitized errors externally
- Don't expose system internals

## Performance

- Use appropriate data types
- Cache static data only
- Consider pagination for large responses
- Monitor response times and error rates

## Versioning

- URL versioning: `/api/v1/endpoint`
- Maintain backward compatibility
- Add optional fields, never remove existing ones

---

**Version**: 1.0 | **Last Updated**: June 25, 2025
