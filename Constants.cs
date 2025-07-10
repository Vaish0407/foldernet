namespace VLite.API
{
    /// <summary>
    /// Constants for VLite API
    /// </summary>
    public static class Constants
    {        /// <summary>
        /// VLite DLL error codes
        /// </summary>
        public static class ErrorCodes
        {
            /// <summary>
            /// Operation completed successfully
            /// </summary>
            public const int vERR_SUCCESS = 0;
            /// <summary>
            /// Undefined error occurred
            /// </summary>
            public const int vERR_UNDEFINED = -1;
            /// <summary>
            /// DLL version mismatch
            /// </summary>
            public const int vERR_DLL_VERSION = -2;
            /// <summary>
            /// Invalid license provided
            /// </summary>
            public const int vERR_INVALID_LICENSE = -3;
            /// <summary>
            /// License has expired
            /// </summary>
            public const int vERR_EXPIRED_LICENSE = -4;
            /// <summary>
            /// Login authentication failed
            /// </summary>
            public const int vERR_LOGIN_FAIL = -5;
            /// <summary>
            /// Socket communication exception
            /// </summary>
            public const int vERR_SOCKET_EXCEPTION = -6;
            /// <summary>
            /// Device communication timeout
            /// </summary>
            public const int vERR_DEV_TIMEOUT = -7;
            /// <summary>
            /// Exceeded maximum write connections
            /// </summary>
            public const int vERR_EXCEED_WRITE_CON = -8;
            /// <summary>
            /// Invalid hub ID specified
            /// </summary>
            public const int vERR_INVALID_HUB = -9;
            /// <summary>
            /// Error unloading DLL
            /// </summary>
            public const int vERR_DLL_UNLOAD = -10;
        }        /// <summary>
        /// Default configuration values
        /// </summary>
        public static class Defaults
        {
            /// <summary>
            /// Default VLite port number
            /// </summary>
            public const int DefaultPort = 9221;
            /// <summary>
            /// Default connection timeout in milliseconds
            /// </summary>
            public const int ConnectionTimeoutMs = 30000;
            /// <summary>
            /// Default command timeout in milliseconds
            /// </summary>
            public const int CommandTimeoutMs = 10000;
            /// <summary>
            /// Maximum number of concurrent connections allowed
            /// </summary>
            public const int MaxConcurrentConnections = 10;
        }        /// <summary>
                 /// API route templates for Core Connection Functions
                 /// </summary>
        public static class Routes
        {
            /// <summary>
            /// Base API route prefix
            /// </summary>
            public const string ApiBase = "api";
            /// <summary>
            /// Route for vcInitialise DLL function
            /// </summary>
            public const string vcInitialise = "vcInitialise";
            /// <summary>
            /// Route for vcTerminate DLL function
            /// </summary>
            public const string vcTerminate = "vcTerminate";
            /// <summary>
            /// Route for vcConnect DLL function
            /// </summary>
            public const string vcConnect = "vcConnect";            /// <summary>
                                                                    /// Route for vcDisconnect DLL function with hub ID parameter
                                                                    /// </summary>
            public const string vcDisconnect = "vcDisconnect/{hubId:int}";

            // Hub Status and Information Functions
            /// <summary>
            /// Route for vcIsHubConnected DLL function
            /// </summary>
            public const string vcIsHubConnected = "vcIsHubConnected/{hubId:int}";
            /// <summary>
            /// Route for vcGetHubSerialFromID DLL function
            /// </summary>
            public const string vcGetHubSerialFromID = "vcGetHubSerialFromID/{hubId:int}";
            /// <summary>
            /// Route for vcGetHubIdFromSerial DLL function
            /// </summary>
            public const string vcGetHubIdFromSerial = "vcGetHubIdFromSerial";
            /// <summary>
            /// Route for vcGetHubIPDetails DLL function
            /// </summary>
            public const string vcGetHubIPDetails = "vcGetHubIPDetails/{hubId:int}";
            /// <summary>
            /// Route for vcGetNTP DLL function
            /// </summary>
            public const string vcGetNTP = "vcGetNTP/{hubId:int}";
            /// <summary>
            /// Route for vcGetHubStartUp DLL function
            /// </summary>
            public const string vcGetHubStartUp = "vcGetHubStartUp/{hubId:int}";
            /// <summary>
            /// Route for vcGetHubTemp DLL function
            /// </summary>
            public const string vcGetHubTemp = "vcGetHubTemp/{hubId:int}";
            /// <summary>
            /// Route for vcGetHubUtil DLL function
            /// </summary>
            public const string vcGetHubUtil = "vcGetHubUtil/{hubId:int}";
            /// <summary>
            /// Route for vcHasHubConfigChanged DLL function
            /// </summary>
            public const string vcHasHubConfigChanged = "vcHasHubConfigChanged/{hubId:int}";
            /// <summary>
            /// Route for vcListDAUInstalled DLL function
            /// </summary>
            public const string vcListDAUInstalled = "vcListDAUInstalled/{hubId:int}";
            /// <summary>
            /// Route for vcListDAUInstalled DLL function
            /// </summary>
            public const string vcGetDAUStartUp = "vcGetDAUStartUp/{hubId:int}/{dauId:int}";
        }
    }
}
