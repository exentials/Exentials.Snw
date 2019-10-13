namespace Exentials.Snw.SnwConnector
{
    /// <summary>
    /// Rfc Return Code
    /// </summary>
    internal enum RfcRc
    {
        /// <summary>
        /// Everything O.K. Used by every function
        /// </summary>
        RfcOk,
        /// <summary>
        /// Error in Network & Communication layer
        /// </summary>
        RfcCommunicationFailure,
        /// <summary>
        /// Unable to logon to SAP system. Invalid password, user locked, etc
        /// </summary>
        RfcLogonFailure,
        /// <summary>
        /// SAP system runtime error (SYSTEM_FAILURE): Shortdump on the backend side
        /// </summary>
        RfcAbapRuntimeFailure,
        /// <summary>
        /// The called function module raised an E-, A- or X-Message
        /// </summary>
        RfcAbapMessage,
        /// <summary>
        /// The called function module raised an Exception (RAISE or MESSAGE ... RAISING)
        /// </summary>
        RfcAbapException,
        /// <summary>
        /// Connection closed by the other side
        /// </summary>
        RfcClosed,
        /// <summary>
        /// No longer used
        /// </summary>
        RfcCanceled,
        /// <summary>
        /// Time out
        /// </summary>
        RfcTimeout,
        /// <summary>
        /// Memory insufficient
        /// </summary>
        RfcMemoryInsufficient,
        /// <summary>
        /// Version mismatch
        /// </summary>
        RfcVersionMismatch,
        /// <summary>
        /// The received data has an unsupported format
        /// </summary>
        RfcInvalidProtocol,
        /// <summary>
        /// A problem while serializing or deserializing RFM parameters
        /// </summary>
        RfcSerializationFailure,
        /// <summary>
        /// An invalid handle was passed to an API call
        /// </summary>
        RfcInvalidHandle,
        /// <summary>
        /// RfcListenAndDispatch did not receive an RFC request during the timeout period
        /// </summary>
        RfcRetry,
        /// <summary>
        /// Error in external custom code. (E.g in the function handlers or tRFC handlers.) Results in SYSTEM_FAILURE
        /// </summary>
        RfcExternalFailure,
        /// <summary>
        /// Inbound tRFC Call already executed (needs to be returned from RFC_ON_CHECK_TRANSACTION in case the TID is already known)
        /// </summary>
        RfcExecuted,
        /// <summary>
        /// Function or structure definition not found (Metadata API)
        /// </summary>
        RfcNotFound,
        /// <summary>
        /// The operation is not supported on that handle
        /// </summary>
        RfcNotSupported,
        /// <summary>
        /// The operation is not supported on that handle at the current point of time (e.g trying a callback on a server handle, while not in a call)
        /// </summary>
        RfcIllegalState,
        /// <summary>
        /// An invalid parameter was passed to an API call, (e.g invalid name, type or length)
        /// </summary>
        RfcInvalidParameter,
        /// <summary>
        /// Codepage conversion error
        /// </summary>
        RfcCodepageConversionFailure,
        /// <summary>
        /// Error while converting a parameter to the correct data type
        /// </summary>
        RfcConversionFailure,
        /// <summary>
        /// The given buffer was to small to hold the entire parameter. Data has been truncated.
        /// </summary>
        RfcBufferTooSmall,
        /// <summary>
        /// Trying to move the current position before the first row of the table
        /// </summary>
        RfcTableMoveBof,
        /// <summary>
        /// Trying to move the current position after the last row of the table
        /// </summary>
        RfcTableMoveEof,
        /// <summary>
        /// Failed to start and attach SAPGUI to the RFC connection 
        /// </summary>
        RfcStartSapguiFailure,
        /// <summary>
        /// "Something" went wrong, but I don't know what...
        /// </summary>
        RfcUnknownError
    }
}
