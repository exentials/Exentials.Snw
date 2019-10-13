using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;


namespace Exentials.Snw.SnwConnector.Native
{
    internal static class UnsafeNativeMethods
    {
        const string SapNwRfc = "sapnwrfc";

        internal class PlatformDlls
        {
            public string Path { get; set; }
            public string[] DllNames { get; set; }
        }

        static UnsafeNativeMethods()
        {
            PlatformDlls libraryInfo = GetNativeLibraryInfo();
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            foreach (var dll in libraryInfo.DllNames)
            {
                var dllPath = Path.Combine(path, "Native", libraryInfo.Path, dll);
                NativeLibrary.Load(dllPath);
            }
        }

        private static PlatformDlls GetNativeLibraryInfo()
        {
            PlatformDlls platformDlls = new PlatformDlls();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && (RuntimeInformation.ProcessArchitecture == Architecture.X64))
            {
                platformDlls.Path = "Win64";
                platformDlls.DllNames = new[] {
                "icudt50.dll",
                "icuin50.dll",
                "icuuc50.dll",
                "libicudecnumber.dll",
                "libsapucum.dll",
                "sapnwrfc.dll"
            };
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && (RuntimeInformation.ProcessArchitecture == Architecture.X86))
            {
                platformDlls.Path = "Win32";
                platformDlls.DllNames = new[] {
                "icudt50.dll",
                "icuin50.dll",
                "icuuc50.dll",
                "libicudecnumber.dll",
                "libsapucum.dll",
                "sapnwrfc.dll"
            };
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && (RuntimeInformation.ProcessArchitecture == Architecture.X64))
            {
                platformDlls.Path = "Linux64";
                platformDlls.DllNames = new[]
                {
                "libicudata.so.50",
                "libicudecnumber.so",
                "libicui18n.so.50",
                "libicuuc.so.50",
                "libsapnwrfc.so",
                "libsapucum.so"
            };
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                platformDlls.Path = "OSX";
                platformDlls.DllNames = new[] {
                "libicudata.50.dylib",
                "libicudecnumber.dylib",
                "libicui18n.50.dylib",
                "libicuuc.50.dylib",
                "libsapnwrfc.dylib",
                "libsapucum.dylib"
            };
            }
            else
            {
                throw new Exception("Unsupported OSPlatform, can't locate sapnwrfc library.");
            }

            return platformDlls;
        }

        //
        // Info API
        //
        [DllImport(SapNwRfc, EntryPoint = "RfcInit")]
        public static extern RfcRc RfcInit();

        [DllImport(SapNwRfc, EntryPoint = "RfcGetVersion")]
        public static extern IntPtr RfcGetVersion(out int majorVersion, out int minorVersion, out int patchLevel);

        //
        // Connection API 
        //
        [DllImport(SapNwRfc, EntryPoint = "RfcOpenConnection")]
        public static extern IntPtr RfcOpenConnection(RfcConnectionParameter[] connectionParams, int paramCount, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcCloseConnection")]
        public static extern RfcRc RfcCloseConnection(IntPtr rfcHandle, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcPing")]
        public static extern RfcRc RfcPing(IntPtr rfcHandle, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetConnectionAttributes")]
        public static extern RfcRc RfcGetConnectionAttributes(IntPtr rfcHandle, out RfcAttributes attributes, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcSetIniPath", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcSetIniPath(string pathName, out RfcErrorInfo errorInfo);

        //
        // Data container API (Function modules, structures & tables) 
        //
        [DllImport(SapNwRfc, EntryPoint = "RfcGetFunctionDesc", CharSet = CharSet.Unicode)]
        public static extern IntPtr RfcGetFunctionDesc(IntPtr rfcHandle, string funcName, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcCreateFunction")]
        public static extern IntPtr RfcCreateFunction(IntPtr funcDescHandle, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcDestroyFunction")]
        public static extern RfcRc RfcDestroyFunction(IntPtr funcHandle, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcInvoke")]
        public static extern RfcRc RfcInvoke(IntPtr rfcHandle, IntPtr funcHandle, out RfcErrorInfo errorInfo);

        //
        // Structure handling functions
        //
        [DllImport(SapNwRfc, EntryPoint = "RfcCreateStructure")]
        public static extern IntPtr RfcCreateStructure(IntPtr typeDescHandle, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcCloneStructure")]
        public static extern IntPtr RfcCloneStructure(IntPtr srcStructureHandle, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcDestroyStructure")]
        public static extern RfcRc RfcDestroyStructure(IntPtr structHandle, out RfcErrorInfo errorInfo);

        //
        // Table handling function
        //
        [DllImport(SapNwRfc, EntryPoint = "RfcGetCurrentRow")]
        public static extern IntPtr RfcGetCurrentRow(IntPtr tableHandle, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcAppendNewRow")]
        public static extern IntPtr RfcAppendNewRow(IntPtr tableHandle, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcDeleteAllRows")]
        public static extern RfcRc RfcDeleteAllRows(IntPtr tableHandle, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcMoveToFirstRow")]
        public static extern RfcRc RfcMoveToFirstRow(IntPtr tableHandle, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcMoveToNextRow")]
        public static extern RfcRc RfcMoveToNextRow(IntPtr tableHandle, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetRowCount")]
        public static extern RfcRc RfcGetRowCount(IntPtr tableHandle, out int rowCount, out RfcErrorInfo errorInfo);

        //
        // Field handling functions
        //
        [DllImport(SapNwRfc, EntryPoint = "RfcGetChars", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetChars(IntPtr dataHandle, string name, StringBuilder charBuffer, int bufferLength, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetNum", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetNum(IntPtr dataHandle, string name, char[] charBuffer, int bufferLength, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetDate", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetDate(IntPtr dataHandle, string name, char[] emptyDate, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetTime", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetTime(IntPtr dataHandle, string name, char[] emptyTime, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetString", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetString(IntPtr dataHandle, string name, StringBuilder stringBuffer, int bufferLength, out int stringLength, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetBytes", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetBytes(IntPtr dataHandle, string name, byte[] byteBuffer, int bufferLength, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetXString", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetXString(IntPtr dataHandle, string name, byte[] byteBuffer, int bufferLength, out int xstringLength, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetInt", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetInt(IntPtr dataHandle, string name, out int value, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetInt1", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetInt1(IntPtr dataHandle, string name, out byte value, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetInt1", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetInt2(IntPtr dataHandle, string name, out short value, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetFloat", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetFloat(IntPtr dataHandle, string name, out double value, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetDecF16", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetDecF16(IntPtr dataHandle, string name, out decimal value, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetDecF34", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetDecF34(IntPtr dataHandle, string name, out decimal value, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetStructure", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetStructure(IntPtr dataHandle, string name, out IntPtr structHandle, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetTable", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetTable(IntPtr dataHandle, string name, out IntPtr tableHandle, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetAbapObject", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetAbapObject(IntPtr dataHandle, string name, out IntPtr objHandle, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetStringLength", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetStringLength(IntPtr dataHandle, string name, out int stringLength, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcSetChars", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcSetChars(IntPtr dataHandle, string name, char[] charValue, int valueLength, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcSetNum", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcSetNum(IntPtr dataHandle, string name, char[] charValue, int valueLength, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcSetNum", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcSetString(IntPtr dataHandle, string name, string stringValue, int valueLength, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcSetDate", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcSetDate(IntPtr dataHandle, string name, char[] date, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcSetTime", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcSetTime(IntPtr dataHandle, string name, char[] time, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcSetBytes", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcSetBytes(IntPtr dataHandle, string name, byte[] byteValue, int valueLength, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcSetXString", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcSetXString(IntPtr dataHandle, string name, byte[] byteValue, int valueLength, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcSetInt", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcSetInt(IntPtr dataHandle, string name, int value, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcSetInt1", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcSetInt1(IntPtr dataHandle, string name, byte value, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcSetInt2", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcSetInt2(IntPtr dataHandle, string name, short value, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcSetFloat", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcSetFloat(IntPtr dataHandle, string name, double value, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcSetDecF16", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcSetDecF16(IntPtr dataHandle, string name, decimal value, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcSetDecF34", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcSetDecF34(IntPtr dataHandle, string name, decimal value, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcSetStructure", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcSetStructure(IntPtr dataHandle, string name, IntPtr value, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcSetTable", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcSetTable(IntPtr dataHandle, string name, IntPtr value, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcSetAbapObject", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcSetAbapObject(IntPtr dataHandle, string name, IntPtr value, out RfcErrorInfo errorInfo);

        //
        // Metadata functions
        //

        [DllImport(SapNwRfc, EntryPoint = "RfcDescribeFunction", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcDescribeFunction(IntPtr funcHandle, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetFunctionName", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetFunctionName(IntPtr funcDescHandle, StringBuilder bufferForName, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetParameterCount", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetParameterCount(IntPtr funcDescHandle, out int count, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetParameterDescByIndex", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetParameterDescByIndex(IntPtr funcDescHandle, int index, out RfcParameterDesc paramDesc, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetParameterDescByName", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetParameterDescByName(IntPtr funcDescHandle, string name, out RfcParameterDesc paramDesc, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetTypeName", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetTypeName(IntPtr typeHandle, StringBuilder bufferForName, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetFieldCount", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetFieldCount(IntPtr typeHandle, out int count, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetFieldDescByIndex", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetFieldDescByIndex(IntPtr typeHandle, int index, out RfcFieldDesc fieldDescr, out RfcErrorInfo errorInfo);

        [DllImport(SapNwRfc, EntryPoint = "RfcGetFieldDescByName", CharSet = CharSet.Unicode)]
        public static extern RfcRc RfcGetFieldDescByName(IntPtr typeHandle, string name, out RfcFieldDesc fieldDescr, out RfcErrorInfo errorInfo);

    }
}
