# VLiteCommon.dll Functions List

This document lists all available functions in the VLiteCommon.dll library for V-MAP G3 Communications and Configuration Software.

## API Endpoint Mapping

The following API endpoints correspond exactly to the VLiteCommon.dll function names:

### Core Connection Functions
- `POST /api/vcInitialise` → vcInitialise()
- `POST /api/vcTerminate` → vcTerminate()  
- `POST /api/vcConnect` → vcConnect()
- `POST /api/vcDisconnect/{hubId}` → vcDisconnect()

### Hub Status and Information Functions
- `GET /api/vcIsHubConnected/{hubId}` → vcIsHubConnected()
- `GET /api/vcGetHubSerialFromID/{hubId}` → vcGetHubSerialFromID()
- `POST /api/vcGetHubIdFromSerial` → vcGetHubIdFromSerial()
- `GET /api/vcGetHubIPDetails/{hubId}` → vcGetHubIPDetails()
- `GET /api/vcGetNTP/{hubId}` → vcGetNTP()
- `GET /api/vcGetHubStartUp/{hubId}` → vcGetHubStartUp()
- `GET /api/vcGetHubTemp/{hubId}` → vcGetHubTemp()
- `GET /api/vcGetHubUtil/{hubId}` → vcGetHubUtil()
- `GET /api/vcHasHubConfigChanged/{hubId}` → vcHasHubConfigChanged()

### DAU (Data Acquisition Unit) Functions
- `GET /api/vcListDAUInstalled/{hubId}` → vcListDAUInstalled()
- `POST /api/vcGetDAUIdFromSerial` → vcGetDAUIdFromSerial()
- `GET /api/vcGetDAUSensorModules/{hubId}/{dauId}` → vcGetDAUSensorModules()
- `GET /api/vcGetDAUUniqueID/{hubId}/{dauId}` → vcGetDAUUniqueID()
- `GET /api/vcGetDAUStartUp/{hubId}/{dauId}` → vcGetDAUStartUp()
- `GET /api/vcGetDAUTemperature/{hubId}/{dauId}` → vcGetDAUTemperature()
- `GET /api/vcListDAUConfigChanged/{hubId}/{dauId}` → vcListDAUConfigChanged()

### Channel Configuration Functions
- `GET /api/vcGetChannelConfig/{hubId}/{dauId}/{channelId}` → vcGetChannelConfig()
- `GET /api/vcGetVirtualGroupDetails/{hubId}/{vgId}` → vcGetVirtualGroupDetails()

### Version and Information Functions
- `GET /api/vcDLLVersionInfo` → vcDLLVersionInfo()
- `GET /api/vcHubOSVersionInfo/{hubId}/{target}` → vcHubOSVersionInfo()
- `GET /api/vcDAUOSVersionInfo/{hubId}/{dauId}/{module}` → vcDAUOSVersionInfo()

### Utility Functions
- `POST /api/vcHUBCmdX/{hubId}` → vcHUBCmdX()
- `GET /api/vcLastErrorDescription` → vcLastErrorDescription()
- `POST /api/vcReadLog/{hubId}` → vcReadLog()

### Configuration Functions
- `POST /api/vcSetDAUEnabledState/{hubId}/{dauId}` → vcSetDAUEnabledState()
- `POST /api/vcSetDAUDataRate/{hubId}/{dauId}` → vcSetDAUDataRate()
- `POST /api/vcResetDAU/{hubId}/{dauId}` → vcResetDAU()

### Internal Utility Functions
- `POST /api/vcgetHubStatus/{hubId}` → vcgetHubStatus()
- `POST /api/vcResetHubObject` → vcResetHubObject()
- `POST /api/vcSetDAUIdFromSerial` → vcSetDAUIdFromSerial()

### Helper Functions
- `POST /api/getDateTimeFromEpochTime` → getDateTimeFromEpochTime()
- `POST /api/getEpochTimeFromDateTime` → getEpochTimeFromDateTime()
- `GET /api/isHubConnectionReadOnly/{hubId}` → isHubConnectionReadOnly()

### Event Callback Endpoints (WebSocket/SignalR)
- `WS /api/vcHubErrorDetected` → vcHubErrorDetected Callback
- `WS /api/vcDAUErrorDetected` → vcDAUErrorDetected Callback
- `WS /api/vcChannelErrorDetected` → vcChannelErrorDetected Callback
- `WS /api/vcHubDisconnect` → vcHubDisconnect Callback
- `WS /api/vcDAUDisconnect` → vcDAUDisconnect Callback

**Total Functions: 42**

## Core Connection Functions

### 1. vcInitialise
- **Description**: Initializes the DLL and performs license validation
- **Signature**: `public int vcInitialise(string LicenseKey, int VersionMajor, int VersionMinor)`
- **Input Parameters**:
  - `LicenseKey` (string): License key for validation
  - `VersionMajor` (int): Major version number
  - `VersionMinor` (int): Minor version number
- **Return Values**: `vERR_SUCCESS`, `vERR_DLL_VERSION`, `vERR_INVALID_LICENSE`, `vERR_EXPIRED_LICENSE`

### 2. vcTerminate
- **Description**: De-initializes data structures and disconnects all hubs
- **Signature**: `public int vcTerminate()`
- **Return Values**: `vERR_SUCCESS`, `vERR_DLL_UNLOAD`

### 3. vcConnect
- **Description**: Connects to individual SPUs using the specified IP address and port
- **Signature**: `public int vcConnect(string IPAddr, int PortNumber, string Password, bool ReadOnly, out int HubID, out string HubSerialNo)`
- **Input Parameters**:
  - `IPAddr` (string): IP address of the SPU
  - `PortNumber` (int): Port number for the connection
  - `Password` (string): Password for authentication
  - `ReadOnly` (bool): Specifies if the connection is read-only
- **Output Parameters**:
  - `HubID` (int): Returns the assigned Hub ID
  - `HubSerialNo` (string): Returns the serial number of the connected SPU
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_LOGIN_FAIL`, `vERR_SOCKET_EXCEPTION`, `vERR_DEV_TIMEOUT`, `vERR_EXCEED_WRITE_CON`

### 4. vcDisconnect
- **Description**: Disconnects from the specified SPU
- **Signature**: `public int vcDisconnect(int HubID)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU to disconnect
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`

## Hub Status and Information Functions

### 5. vcIsHubConnected
- **Description**: Checks if the SPU is currently connected
- **Signature**: `public int vcIsHubConnected(int HubID, out bool IsConnected)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
- **Output Parameters**:
  - `IsConnected` (bool): Connection status
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`

### 6. vcGetHubSerialFromID
- **Description**: Retrieves the SPU serial number matching the supplied HubID
- **Signature**: `public int vcGetHubSerialFromID(int HubID, out string HubSerialNo)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
- **Output Parameters**:
  - `HubSerialNo` (string): Serial number of the SPU
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`

### 7. vcGetHubIdFromSerial
- **Description**: Retrieves the HubID matching the supplied serial number
- **Signature**: `public int vcGetHubIdFromSerial(string HubSerialNo, out int HubID)`
- **Input Parameters**:
  - `HubSerialNo` (string): Serial number of the SPU
- **Output Parameters**:
  - `HubID` (int): ID of the SPU
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`

### 8. vcGetHubIPDetails
- **Description**: Retrieves network information from the specified SPU
- **Signature**: `public int vcGetHubIPDetails(int HubID, out string IPAddr, out string SubNetMask, out string Gateway, out string HostName, out string Domain, out bool DHCPStatus, out uint PortNumber, out string DNSServer1, out string DNSServer2)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
- **Output Parameters**:
  - `IPAddr` (string): IP address
  - `SubNetMask` (string): Subnet mask
  - `Gateway` (string): Gateway address
  - `HostName` (string): Host name
  - `Domain` (string): Domain name
  - `DHCPStatus` (bool): DHCP enabled status
  - `PortNumber` (uint): Port number
  - `DNSServer1` (string): Primary DNS server
  - `DNSServer2` (string): Secondary DNS server
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`

### 9. vcGetNTP
- **Description**: Retrieves the NTP server and synchronization frequency from the SPU
- **Signature**: `public int vcGetNTP(int HubID, out string NTPServer, out int NTPPollInterval)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
- **Output Parameters**:
  - `NTPServer` (string): NTP server address
  - `NTPPollInterval` (int): NTP polling interval
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`

### 10. vcGetHubStartUp
- **Description**: Returns the last startup time of the SPU
- **Signature**: `public int vcGetHubStartUp(int HubID, out DateTime LastStart)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
- **Output Parameters**:
  - `LastStart` (DateTime): Last startup time of the SPU
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`

### 11. vcGetHubTemp
- **Description**: Returns the current internal temperature of the SPU
- **Signature**: `public int vcGetHubTemp(int HubID, out int Temperature_degC)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
- **Output Parameters**:
  - `Temperature_degC` (int): Current internal temperature in degrees Celsius
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`

### 12. vcGetHubUtil
- **Description**: Returns the current percentage utilization of the SPU
- **Signature**: `public int vcGetHubUtil(int HubID, out int Utilization)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
- **Output Parameters**:
  - `Utilization` (int): Current percentage utilization
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`

### 13. vcHasHubConfigChanged
- **Description**: Checks if the SPU configuration settings have changed
- **Signature**: `public int vcHasHubConfigChanged(int HubID, out bool isConfigChanged)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
- **Output Parameters**:
  - `isConfigChanged` (bool): Configuration change status
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`

## DAU (Data Acquisition Unit) Functions

### 14. vcListDAUInstalled
- **Description**: Determines which slots in the SPU are active
- **Signature**: `public int vcListDAUInstalled(int HubID, out UInt16 Status, out UInt16 State)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
- **Output Parameters**:
  - `Status` (UInt16): DAU status information
  - `State` (UInt16): DAU state information
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`

### 15. vcGetDAUIdFromSerial
- **Description**: Retrieves the DAUID and HUBID matching the supplied DAU serial number
- **Signature**: `public int vcGetDAUIdFromSerial(string DAUSerialNo, out int DAUID, out int HubID)`
- **Input Parameters**:
  - `DAUSerialNo` (string): Serial number of the DAU
- **Output Parameters**:
  - `DAUID` (int): ID of the DAU
  - `HubID` (int): ID of the Hub
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`

### 16. vcGetDAUSensorModules
- **Description**: Retrieves the number of sensor modules connected to the specified DAU
- **Signature**: `public int vcGetDAUSensorModules(int HubID, int DAUID, out int SensorModules)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
  - `DAUID` (int): ID of the DAU
- **Output Parameters**:
  - `SensorModules` (int): Number of sensor modules
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`, `vERR_INVALID_DAU`

### 17. vcGetDAUUniqueID
- **Description**: Retrieves the unique ID of the specified DAU
- **Signature**: `public int vcGetDAUUniqueID(int HubID, int DAUID, out string DAU_uniqueId)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
  - `DAUID` (int): ID of the DAU
- **Output Parameters**:
  - `DAU_uniqueId` (string): Unique ID of the DAU
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`, `vERR_INVALID_DAU`

### 18. vcGetDAUStartUp
- **Description**: Returns the last detected startup time of the DAU
- **Signature**: `public int vcGetDAUStartUp(int HubID, int DAUID, out DateTime LastStart)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
  - `DAUID` (int): ID of the DAU
- **Output Parameters**:
  - `LastStart` (DateTime): Last startup time of the DAU
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`, `vERR_INVALID_DAU`

### 19. vcGetDAUTemperature
- **Description**: Returns the current internal temperature of the DAU and microcontroller
- **Signature**: `public int vcGetDAUTemperature(int HubID, int DAUID, out int DAUTemperature, out int DAUCoreTemperature)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
  - `DAUID` (int): ID of the DAU
- **Output Parameters**:
  - `DAUTemperature` (int): DAU temperature
  - `DAUCoreTemperature` (int): DAU core temperature
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`, `vERR_INVALID_DAU`

### 20. vcListDAUConfigChanged
- **Description**: Returns a 16-bit value indicating the configuration change status of each DAU
- **Signature**: `public int vcListDAUConfigChanged(int HubID, UInt16 DAUID, out int ConfigChangedMask)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
  - `DAUID` (UInt16): ID of the DAU
- **Output Parameters**:
  - `ConfigChangedMask` (int): Configuration change status mask
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`

## Channel Configuration Functions

### 21. vcGetChannelConfig
- **Description**: Reads the current configuration of the specified channel
- **Signature**: `public int vcGetChannelConfig(int HubID, int DAUID, int ChNumber, out string channelName, out int gain, out int bw, out int offset, out int powerOn, out int mode, out int viMode, out int samplingPeriod, out int enable, out string engineeringPrecision, out string equationString, out string measurementUnit)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
  - `DAUID` (int): ID of the DAU
  - `ChNumber` (int): Channel number
- **Output Parameters**:
  - `channelName` (string): Channel name
  - `gain` (int): Channel gain
  - `bw` (int): Filter bandwidth
  - `offset` (int): Channel offset
  - `powerOn` (int): Power on status
  - `mode` (int): Channel mode
  - `viMode` (int): VI mode
  - `samplingPeriod` (int): Sampling period
  - `enable` (int): Channel enable status
  - `engineeringPrecision` (string): Engineering precision
  - `equationString` (string): Equation string
  - `measurementUnit` (string): Measurement unit
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`, `vERR_INVALID_DAU`, `vERR_INVALID_CHANNEL`

### 22. vcGetVirtualGroupDetails
- **Description**: Reads data relating to the virtual groups
- **Signature**: `public int vcGetVirtualGroupDetails(int HubID, int VGID, out string VGName)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
  - `VGID` (int): Virtual Group ID
- **Output Parameters**:
  - `VGName` (string): Virtual Group name
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`, `vERR_INVALID_VG`

## Version and Information Functions

### 23. vcDLLVersionInfo
- **Description**: Returns the version number of the VLiteCommon.DLL
- **Signature**: `public int vcDLLVersionInfo(out int VersionMajor, out int VersionMinor, out int VersionBuild)`
- **Output Parameters**:
  - `VersionMajor` (int): Major version number
  - `VersionMinor` (int): Minor version number
  - `VersionBuild` (int): Build version number
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`

### 24. vcHubOSVersionInfo
- **Description**: Returns the version number of the firmware in the SPU
- **Signature**: `public int vcHubOSVersionInfo(int HubID, byte FirmwareTarget, out string Version)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
  - `FirmwareTarget` (byte): Firmware component target
- **Output Parameters**:
  - `Version` (string): Firmware version
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`

### 25. vcDAUOSVersionInfo
- **Description**: Returns the version number of the firmware in the DAU
- **Signature**: `public int vcDAUOSVersionInfo(int HubID, int DAUID, int SensorModule, out string Version)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
  - `DAUID` (int): ID of the DAU
  - `SensorModule` (int): ID of sensor module
- **Output Parameters**:
  - `Version` (string): Firmware version
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`, `vERR_INVALID_DAU`

## Utility Functions

### 26. vcHUBCmdX
- **Description**: Allows Linux commands to be passed through to the SPU and returns the output
- **Signature**: `public int vcHUBCmdX(int HubID, string LinuxCommand, out string Output)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
  - `LinuxCommand` (string): Linux command to execute
- **Output Parameters**:
  - `Output` (string): Command output
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`

### 27. vcLastErrorDescription
- **Description**: Provides additional details on the last error encountered by the DLL
- **Signature**: `public int vcLastErrorDescription(out string ErrorDescription)`
- **Output Parameters**:
  - `ErrorDescription` (string): Error description
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`

### 28. vcReadLog
- **Description**: Reads the contents of the log file between two optional timestamps
- **Signature**: `public int vcReadLog(int HubID, DateTime StartTime, DateTime EndTime, out string LogData)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
  - `StartTime` (DateTime): Start timestamp
  - `EndTime` (DateTime): End timestamp
- **Output Parameters**:
  - `LogData` (string): Log data
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`

## Configuration Functions

### 29. vcSetDAUEnabledState
- **Description**: Enables or disables power to individual DAUs
- **Signature**: `public int vcSetDAUEnabledState(int HubID, int DAUID, bool Enabled)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
  - `DAUID` (int): ID of the DAU
  - `Enabled` (bool): Enable/disable state
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`, `vERR_INVALID_DAU`

### 30. vcSetDAUDataRate
- **Description**: Sets the baud rate for communications between the SPU and the DAU
- **Signature**: `public int vcSetDAUDataRate(int HubID, int DAUID, UInt32 BaudRate)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
  - `DAUID` (int): ID of the DAU
  - `BaudRate` (UInt32): Baud rate
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`, `vERR_INVALID_DAU`

### 31. vcResetDAU
- **Description**: Restarts the specified DAU by temporarily cutting power
- **Signature**: `public int vcResetDAU(int HubID, int DAUID)`
- **Input Parameters**:
  - `HubID` (int): ID of the SPU
  - `DAUID` (int): ID of the DAU
- **Return Values**: `vERR_SUCCESS`, `vERR_UNDEFINED`, `vERR_INVALID_HUB`, `vERR_INVALID_DAU`

## Internal Utility Functions

### 32. vcgetHubStatus
- **Description**: Requests SPU to send the status of DAUs, channels, and operations
- **Signature**: `public void vcgetHubStatus(int HubId, out int outstandingEventId)`
- **Input Parameters**:
  - `HubId` (int): ID of the SPU
- **Output Parameters**:
  - `outstandingEventId` (int): Outstanding event ID

### 33. vcResetHubObject
- **Description**: Resets an existing HubObject instance when a hub disconnect is detected
- **Signature**: `public void vcResetHubObject(HubObject hubObject)`
- **Input Parameters**:
  - `hubObject` (HubObject): Hub object to reset

### 34. vcSetDAUIdFromSerial
- **Description**: Stores the DAUID and serial number of the DAU to the DAUObject instance
- **Input Parameters**:
  - DAU serial number and ID information

## Helper Functions

### 35. getDateTimeFromEpochTime
- **Description**: Converts epoch time to DateTime format
- **Input Parameters**:
  - Epoch time value
- **Return**: DateTime object

### 36. getEpochTimeFromDateTime
- **Description**: Converts DateTime to epoch time
- **Input Parameters**:
  - DateTime object
- **Return**: Epoch time value

### 37. isHubConnectionReadOnly
- **Description**: Determines if the hub connection is read-only
- **Input Parameters**:
  - Hub connection information
- **Return**: Boolean indicating read-only status

## Event Callbacks

### 38. vcHubErrorDetected Callback
- **Description**: Raised when the Host identifies an error with communicating with the SPU
- **Parameters**:
  - `HubID` (int): ID of the SPU
  - `HubError` (int): Error code
  - `AdditionalData` (int): Additional error data

### 39. vcDAUErrorDetected Callback
- **Description**: Raised when the SPU identifies an internal error with a DAU
- **Parameters**:
  - `HubID` (int): ID of the SPU
  - `DAUID` (int): ID of the DAU
  - `DAUError` (int): Error code

### 40. vcChannelErrorDetected Callback
- **Description**: Raised when the SPU identifies an internal error with a DAU channel
- **Parameters**:
  - `HubID` (int): ID of the SPU
  - `DAUID` (int): ID of the DAU
  - `DAUError` (int): Error code

### 41. vcHubDisconnect Callback
- **Description**: Raised when the DLL detects that a SPU is no longer accessible
- **Parameters**:
  - `HubID` (int): ID of the SPU
  - `Reason` (int): Disconnect reason

### 42. vcDAUDisconnect Callback
- **Description**: Raised when the DLL detects that a DAU is no longer accessible
- **Parameters**:
  - `HubID` (int): ID of the SPU
  - `DAUID` (int): ID of the DAU
  - `Reason` (int): Disconnect reason

## Error Codes

Common error codes returned by VLiteCommon functions:

- `vERR_SUCCESS`: Operation completed successfully
- `vERR_UNDEFINED`: Undefined error
- `vERR_INVALID_HUB`: Invalid Hub ID
- `vERR_INVALID_DAU`: Invalid DAU ID
- `vERR_INVALID_CHANNEL`: Invalid channel number
- `vERR_INVALID_VG`: Invalid Virtual Group ID
- `vERR_LOGIN_FAIL`: Login failed
- `vERR_SOCKET_EXCEPTION`: Socket communication error
- `vERR_DEV_TIMEOUT`: Device timeout
- `vERR_DLL_VERSION`: DLL version mismatch
- `vERR_INVALID_LICENSE`: Invalid license
- `vERR_EXPIRED_LICENSE`: Expired license
- `vERR_DLL_UNLOAD`: DLL unload error
- `vERR_EXCEED_WRITE_CON`: Exceeded write connections limit

## Notes

1. All functions return an integer error code where 0 (vERR_SUCCESS) indicates success
2. Functions with `out` parameters return data through reference parameters
3. Hub ID values typically range from 1 to 32 (MAX_HUB_OBJECTS)
4. DAU ID values depend on the SPU configuration
5. The library requires proper initialization with `vcInitialise` before use
6. Always call `vcTerminate` before application shutdown
