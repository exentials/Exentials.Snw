using System.Runtime.InteropServices;

namespace Exentials.Snw.SnwConnector
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct RfcConnectionParameter
    {
        [MarshalAs(UnmanagedType.LPTStr)]
        public string Name;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string Value;
    }
}
