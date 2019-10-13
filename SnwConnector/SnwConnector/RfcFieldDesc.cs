using System;
using System.Runtime.InteropServices;

namespace Exentials.Snw.SnwConnector
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct RfcFieldDesc
    {
        /// <summary>
        /// Field name, null-terminated string
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30 + 1)] 
        internal readonly string Name;
        /// <summary>
        /// Field data type
        /// </summary>
        [MarshalAs(UnmanagedType.U4)] 
        internal readonly RfcType Type;
        /// <summary>
        /// Field length in bytes in a 1-byte-per-SAP_CHAR system
        /// </summary>
        internal readonly int NUCLength;
        /// <summary>
        /// Field offset in bytes in a 1-byte-per-SAP_CHAR system
        /// </summary>
        private readonly int NUCOffset;
        /// <summary>
        /// Field length in bytes in a 2-byte-per-SAP_CHAR system
        /// </summary>
        internal readonly int UCLength;
        /// <summary>
        /// Field offset in bytes in a 2-byte-per-SAP_CHAR system
        /// </summary>
        private readonly int UCOffset;
        /// <summary>
        /// If the field is of type "packed number" (BCD), this member gives the number of decimals.
        /// </summary>
        internal readonly int Decimals;
        /// <summary>
        /// Pointer to an RFC_STRUCTURE_DESC structure for the nestesd sub-type if the type field is RFCTYPE_STRUCTURE or RFCTYPE_TABLE */
        /// </summary>
        private readonly IntPtr typeDescHandle;
        /// <summary>
        /// Not used by the NW RFC library. This parameter can be used by applications that want to store additional information in the repository (like F4 help values, e.g.).
        /// </summary>
        private readonly IntPtr extendedDescription;
    }
}
