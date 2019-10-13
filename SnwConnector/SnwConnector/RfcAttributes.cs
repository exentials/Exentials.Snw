using System.Runtime.InteropServices;

namespace Exentials.Snw.SnwConnector
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct RfcAttributes
    {
        /// <summary>
        /// RFC destination
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64 + 1)]
        internal readonly string Destination;
        /// <summary>
        /// Own host name
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100 + 1)]
        internal readonly string Host;
        /// <summary>
        /// Partner host name
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100 + 1)]
        internal readonly string PartnerHost;
        /// <summary>
        /// R/3 system number
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2 + 1)]
        internal readonly string SysNumber;
        /// <summary>
        /// R/3 system ID
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8 + 1)]
        internal readonly string sysId;
        /// <summary>
        /// Client ("Mandant")
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3 + 1)]
        internal readonly string Client;
        /// <summary>
        /// User
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12 + 1)]
        internal readonly string User;
        /// <summary>
        /// Language
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2 + 1)]
        internal readonly string Language;
        /// <summary>
        /// Trace level (0-3)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1 + 1)]
        internal readonly string trace;
        /// <summary>
        /// 2-byte ISO-Language
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2 + 1)]
        internal readonly string IsoLanguage;
        /// <summary>
        /// Own code page
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4 + 1)]
        internal readonly string Codepage;
        /// <summary>
        /// Partner code page
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4 + 1)]
        internal readonly string PartnerCodepage;
        /// <summary>
        /// C/S: RFC Client / RFC Server
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1 + 1)]
        internal readonly string RfcRole;
        /// <summary>
        /// 2/3/E/R: R/2,R/3,Ext,Reg.Ext
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1 + 1)]
        internal readonly string Type;
        /// <summary>
        /// 2/3/E/R: R/2,R/3,Ext,Reg.Ext
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1 + 1)]
        internal readonly string PartnerType;
        /// <summary>
        /// My system release
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4 + 1)]
        internal readonly string rel;
        /// <summary>
        /// Partner system release
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4 + 1)]
        internal readonly string partnerRel;
        /// <summary>
        /// Partner kernel release
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4 + 1)]
        internal readonly string KernelRel;
        /// <summary>
        /// CPI-C Conversation ID
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8 + 1)]
        internal readonly string cpicConvId;
        /// <summary>
        /// Name of the calling APAB program (report, module pool)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128 + 1)]
        internal readonly string progName;
        /// <summary>
        /// Number of bytes per character in the backend's current codepage. Note this is different from the semantics of the PCS parameter.                                         
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1 + 1)]
        internal readonly string partnerBytesPerChar;
        /// <summary>
        /// Reserved for later use
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 84)]
        internal readonly string Reserved;
    }
}
