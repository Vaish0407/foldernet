# How `using VMapG3;` is Used

Based on the code you've provided, `using VMapG3;` is used to import the **VMapG3 namespace** into your C# files. This document explains exactly how it's being used in your VLiteAPI service.

## Overview

The `using VMapG3;` statement allows you to use classes from the VMapG3 namespace directly without fully qualifying them with the full namespace path.

## 1. Direct Class Instantiation

In your `VLiteCommonService.cs`, the `using VMapG3;` statement enables direct usage:

```csharp
using VMapG3;  // This import statement

namespace VLite.API.Services
{
    public class VLiteCommonService
    {
        private readonly VLiteCommon _vliteCommon;  // ← This uses VMapG3.VLiteCommon
        
        public VLiteCommonService(...)
        {
            _vliteCommon = new VLiteCommon();  // ← Creates instance of VMapG3.VLiteCommon
        }
    }
}
```

## 2. Without the using statement

Without the `using VMapG3;` statement, you would need to write the full namespace path:

```csharp
// Without using VMapG3;
private readonly VMapG3.VLiteCommon _vliteCommon;
_vliteCommon = new VMapG3.VLiteCommon();
```

## 3. Available Classes and Enums from VMapG3 Namespace

By using `using VMapG3;`, your service can access:

### Classes:
- `VLiteCommon` - Main communications class
- `HubObject` - Hub object structure  
- `ChannelDRStructure` - Channel data structure

### Enums:
- `ErrorCodes` - Error code definitions
- `FWU_states` - Firmware update states
- `FirmwareTarget` - Firmware target types
- `FILE_TYPES` - File type definitions
- `eGenericParms` - Generic parameter definitions

## 4. Actual Usage in Your Service

Your service primarily uses the `VLiteCommon` class to call the underlying DLL functions:

```csharp
// These method calls are possible because of "using VMapG3;"
int result = _vliteCommon.vcInitialise(request.LicenseKey, request.VersionMajor, request.VersionMinor);
int result = _vliteCommon.vcConnect(request.IPAddr, request.PortNumber, request.Password, request.ReadOnly, out int hubId, out string hubSerialNo);
int result = _vliteCommon.vcDisconnect(hubId);
int result = _vliteCommon.vcTerminate();
```

## 5. Error Code References

You also likely use the error codes defined in the namespace:

```csharp
// Available because of "using VMapG3;"
if (result == ErrorCodes.vERR_SUCCESS)
{
    // Success handling
}
```

## 6. What is VMapG3?

**VMapG3** is a **C# namespace** used in a custom software system for **V-MAP(s) G3 Communications and Configuration Software**. It appears to be a proprietary communications system for industrial/diagnostic equipment.

### Key Details:
- **Company**: Originally developed by **Datalink Electronics** for **Score Diagnostics**
- **Copyright**: ©2017, All Rights Reserved  
- **Purpose**: Implementation of functions for V-MAP(s) G3 Communications and Configuration Software (Document: 9102-01)
- **Industry**: Related to diagnostic equipment/industrial communications

### What's in your workspace:
- **VLiteCommon.dll** - The main C# library using `namespace VMapG3`
- **VLiteAPI** - A REST API wrapper that provides HTTP endpoints for the VMapG3 functionality
- **Java-CS-Bridge integration** - For calling VMapG3 functions from Java

## 7. Not a NuGet Package

This is **not a publicly available NuGet package** - it's a proprietary system for specific industrial/diagnostic hardware communications. The namespace `VMapG3` is used internally within your company's software to organize classes related to this V-MAP G3 communications protocol.

## Summary

**In summary:** The `using VMapG3;` statement imports the VMapG3 namespace so you can use `VLiteCommon` and other classes directly without prefixing them with `VMapG3.` every time. This makes the code cleaner and more readable while accessing the proprietary V-MAP G3 communications functionality.
