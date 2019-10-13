namespace Exentials.Snw.SnwConnector
{
    internal enum RfcErrorGroup
    {
        /// <summary>
        /// OK
        /// </summary>
        OK,
        /// <summary>
        /// ABAP Exception raised in ABAP function modules
        /// </summary>
        ABAP_APPLICATION_FAILURE,
        /// <summary>
        /// ABAP Message raised in ABAP function modules or in ABAP runtime of the backend (e.g Kernel)
        /// </summary>
        ABAP_RUNTIME_FAILURE,
        /// <summary>
        /// Error message raised when logon fails 
        /// </summary>
        LOGON_FAILURE,
        /// <summary>
        /// Problems with the network connection (or backend broke down and killed the connection)
        /// </summary>
        COMMUNICATION_FAILURE,
        /// <summary>
        /// Problems in the RFC runtime of the external program (i.e "this" library)
        /// </summary>
        EXTERNAL_RUNTIME_FAILURE,
        /// <summary>
        /// Problems in the external program (e.g in the external server implementation)
        /// </summary>
        EXTERNAL_APPLICATION_FAILURE
    }
}
