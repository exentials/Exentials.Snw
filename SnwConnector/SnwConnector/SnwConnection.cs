using Exentials.Snw.SnwConnector.Native;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Exentials.Snw.SnwConnector
{
    /// <summary>
    /// The SnwConnection provide and handle a connection to a SAP R/3 system.
    /// The class wrap SAP NetWeaver RFC dll and give access to the RFC calling infrastructure.
    /// </summary>
    public sealed class SnwConnection : IDisposable
    {
        private IntPtr _rfcHandleConnection;
        private string _ashost;
        private int _systemNumber;
        private string _client;
        private string _userName;
        private string _password;
        private string _language;
        private bool _disposed;

        public SnwConnection()
        {
            _rfcHandleConnection = IntPtr.Zero;
            _ashost = string.Empty;
            _systemNumber = 0;
            _client = string.Empty;
            _userName = string.Empty;
            _password = string.Empty;
            _language = string.Empty;
        }

        public SnwConnection(string host, int systemNumber, string client, string user, string password, string language)
        {
            _rfcHandleConnection = IntPtr.Zero;
            _ashost = host;
            _systemNumber = systemNumber;
            _client = client;
            _userName = user;
            _password = password;
            _language = language;
        }

        /// <summary>
        /// Get SAP NetWeaver DLL version.
        /// </summary>
        /// <exception cref="Exentials.Snw.SnwConnector.SnwConnectorException"></exception>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static IDictionary<string, object> GetLibraryInfo()
        {
            var results = new Dictionary<string, object>();
            results["Version"] = string.Empty;
            results["RuntimePlatform"] = Platform;
            try
            {
                int majorVersion;
                int minorVersion;
                int patchLevel;
                IntPtr res = UnsafeNativeMethods.RfcGetVersion(out majorVersion, out minorVersion, out patchLevel);
                results["Version"] = Marshal.PtrToStringAuto(res);
                results["MajorVersion"] = majorVersion;
                results["MinorVersion"] = minorVersion;
                results["PatchLevel"] = patchLevel;
            }
            catch (DllNotFoundException dllNotFound)
            {
                throw new SnwConnectorException(Messages.RfcDllNotFound, dllNotFound);
            }
            catch (BadImageFormatException badImageFormat)
            {
                throw new SnwConnectorException(string.Format(CultureInfo.CurrentCulture, Messages.RfcDllBadFormat, Platform), badImageFormat);
            }
            return results;
        }

        /// <summary>
        /// Host name or ip address
        /// </summary>
        public string Host
        {
            get { return _ashost; }
            set { _ashost = value; }
        }

        /// <summary>
        /// Get or Set SAP system number connection
        /// </summary>
        public int SystemNumber
        {
            get { return _systemNumber; }
            set { _systemNumber = value; }
        }

        /// <summary>
        /// Get or Set SAP client number connection
        /// </summary>
        public string Client
        {
            get { return _client; }
            set { _client = value; }
        }

        /// <summary>
        /// User name 
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        /// <summary>
        /// Password
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// Language connection (ex. EN, IT)
        /// </summary>
        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        /// <summary>
        /// Open the Rfc connection
        /// </summary>
        public void Open()
        {
            if (_rfcHandleConnection != IntPtr.Zero)
            {
                throw new SnwConnectorException(Messages.RfcConnectionAlreadyOpen);
            }

            var loginParams = new RfcConnectionParameter[6];

            loginParams[0].Name = "ASHOST";
            loginParams[0].Value = _ashost;
            loginParams[1].Name = "sysnr";
            loginParams[1].Value = _systemNumber.ToString(CultureInfo.InvariantCulture);
            loginParams[2].Name = "client";
            loginParams[2].Value = _client;
            loginParams[3].Name = "user";
            loginParams[3].Value = _userName;
            loginParams[4].Name = "passwd";
            loginParams[4].Value = _password;
            loginParams[5].Name = "lang";
            loginParams[5].Value = _language;

            RfcErrorInfo errorInfo;
            try
            {
                _rfcHandleConnection = UnsafeNativeMethods.RfcOpenConnection(loginParams, loginParams.Length, out errorInfo);
                GetConnectionInfo();
            }
            catch (DllNotFoundException dllNotFound)
            {
                throw new SnwConnectorException(Messages.RfcDllNotFound, dllNotFound);
            }

            if (errorInfo.Code != RfcRc.RfcOk)
            {
                throw new SnwConnectorException(errorInfo.Code);
            }
        }

        /// <summary>
        /// Close the Rfc connection
        /// </summary>
        public void Close()
        {
            if (_rfcHandleConnection != IntPtr.Zero)
            {
                RfcErrorInfo errorInfo;
                UnsafeNativeMethods.RfcCloseConnection(_rfcHandleConnection, out errorInfo);
                _rfcHandleConnection = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Ping the host to test the communication
        /// </summary>
        /// <returns></returns>
        public bool Ping()
        {
            if (_rfcHandleConnection != IntPtr.Zero)
            {
                RfcErrorInfo errorInfo;
                try
                {
                    UnsafeNativeMethods.RfcPing(_rfcHandleConnection, out errorInfo);
                }
                catch (DllNotFoundException dllNotFound)
                {
                    throw new SnwConnectorException(Messages.RfcDllNotFound, dllNotFound);
                }
                if (errorInfo.Code == RfcRc.RfcOk) return true;
            }
            return false;
        }

        ~SnwConnection()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                }

                // Call the appropriate methods to clean up unmanaged resources.
                Close();

                // Disposing has been done.
                _disposed = true;
            }
        }

        #region Private Properties

        /// <summary>
        /// Test if running in a 64bit enviroment.
        /// </summary>
        private static string Platform
        {
            get { return Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") ?? "x86"; }
        }
        #endregion

        internal IntPtr RfcHandle
        {
            get { return _rfcHandleConnection; }
        }

        public string KernelRelease { get; private set; }

        #region Private methods

        private void GetConnectionInfo()
        {
            RfcErrorInfo errorInfo;
            RfcAttributes rfcAttributesConnection;
            UnsafeNativeMethods.RfcGetConnectionAttributes(_rfcHandleConnection, out rfcAttributesConnection, out errorInfo);
            errorInfo.IfErrorThrowException();

            KernelRelease = rfcAttributesConnection.KernelRel;
        }

        #endregion

    }
}
