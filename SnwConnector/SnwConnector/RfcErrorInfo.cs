using System.Runtime.InteropServices;

namespace Exentials.Snw.SnwConnector
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct RfcErrorInfo
    {
        /// <summary>
        /// Error code. Should be the same as the API returns if the API has return type RFC_RC
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public RfcRc Code;
        /// <summary>
        /// Error group
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public RfcErrorGroup Group;
        /// <summary>
        /// Error key
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string Key;
        /// <summary>
        /// Error message
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string Message;
        /// <summary>
        /// ABAP message ID , or class
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20 + 1)]
        public string AbapMsgClass;
        /// <summary>
        /// ABAP message type, e.g. 'E', 'A' or 'X'
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1 + 1)]
        public string AbapMsgType;
        /// <summary>
        /// ABAP message number
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50 + 1)]
        public string AbapMsgNumber;
        /// <summary>
        /// ABAP message details field 1, corresponds to SY-MSGV1 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50 + 1)]
        public string AbapMsgV1;
        /// <summary>
        /// ABAP message details field 2, corresponds to SY-MSGV2 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50 + 1)]
        public string AbapMsgV2;
        /// <summary>
        /// ABAP message details field 3, corresponds to SY-MSGV3 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50 + 1)]
        public string AbapMsgV3;
        /// <summary>
        /// ABAP message details field 4, corresponds to SY-MSGV4 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50 + 1)]
        public string AbapMsgV4;

        public override string ToString()
        {
            return (Code == RfcRc.RfcOk) ? "OK" : Message;
        }

        public void IfErrorThrowException()
        {
            if (Code != RfcRc.RfcOk)
                throw new SnwConnectorException(this);
        }
    }
}
