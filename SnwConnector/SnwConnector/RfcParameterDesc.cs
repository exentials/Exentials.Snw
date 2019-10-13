using System;
using System.Runtime.InteropServices;

namespace Exentials.Snw.SnwConnector
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct RfcParameterDesc
    {
        /// <summary>
        /// Parameter name, null-terminated string    
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30 + 1)]
        public string Name;
        /// <summary>
        /// Parameter data type
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public RfcType Type;
        /// <summary>
        /// Specifies whether the parameter is an input, output or bi-directinal parameter
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public RfcDirection Direction;
        /// <summary>
        /// Parameter length in bytes in a 1-byte-per-SAP_CHAR system
        /// </summary>
        public int Length;
        ///Parameter length in bytes in a 2-byte-per-SAP_CHAR system
        public int UCLength;
        /// <summary>
        /// Gives the number of decimals in case or a packed number (BCD)
        /// </summary>
        public int Decimals;
        /// <summary>
        /// Handle to the structure definition in case this parameter is a structure or table
        /// </summary>
        public IntPtr TypeDescHandle;
        /// <summary>
        /// Default value as defined in SE37
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30 + 1)]
        public string DefaultValue;
        /// <summary>
        /// Description text of the parameter as defined in SE37. Null-terminated string.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 79 + 1)]
        public string ParameterText;
        /// <summary>
        /// Specifies whether this parameter is defined as optional in SE37. 1 is optional, 0 non-optional
        /// </summary>
        public byte Optional;
        /// <summary>
        /// This field can be used by the application programmer (i.e. you) to store arbitrary extra information.
        /// </summary>
        private IntPtr extendedDescription;
    }
}
