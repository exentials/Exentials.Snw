using Exentials.Snw.SnwConnector.Native;
using System;

namespace Exentials.Snw.SnwConnector
{
    /// <summary>
    /// SnwFunction handle a RFC calling function
    /// </summary>
    /// <exception cref="Exentials.Snw.SnwConnector.SnwConnectorException" />
    public class SnwFunction : IDataParameter, IDataContainer, IDisposable
    {
        private bool _disposed;
        private IntPtr _functionHandler;
        private readonly IntPtr _functionDescriptionHandler;

        private readonly string _functionName;
        private readonly SnwConnection _connection;

        /// <summary>
        /// Instantiate a remote function module
        /// </summary>
        /// <param name="functionName">Function module name</param>
        /// <param name="connection">A SnwConnection instance</param>
        public SnwFunction(string functionName, SnwConnection connection)
        {
            if (string.IsNullOrEmpty(functionName))
                throw new ArgumentNullException("functionName");
            if (connection == null)
                throw new ArgumentNullException("connection");

            _functionName = functionName;
            _connection = connection;

            if (!_connection.Ping())
            {
                throw new SnwConnectorException(Messages.RfcConnectionIsNotOpen);
            }

            RfcErrorInfo errorInfo;
            _functionDescriptionHandler = UnsafeNativeMethods.RfcGetFunctionDesc(_connection.RfcHandle, functionName, out errorInfo);
            errorInfo.IfErrorThrowException();

            _functionHandler = UnsafeNativeMethods.RfcCreateFunction(_functionDescriptionHandler, out errorInfo);
            errorInfo.IfErrorThrowException();
        }

        #region Public properties

        public string Name
        {
            get { return _functionName; }
        }

        #endregion

        public void Execute()
        {
            RfcErrorInfo errorInfo;
            UnsafeNativeMethods.RfcInvoke(_connection.RfcHandle, _functionHandler, out errorInfo);
            errorInfo.IfErrorThrowException();
        }

        IntPtr IDataContainer.DataHandle
        {
            get { return _functionHandler; }
        }

        internal IntPtr DescriptionHandle
        {
            get
            {
                return _functionDescriptionHandler;
            }
        }

        #region Class Disposing

        ~SnwFunction()
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
                if (_functionHandler != IntPtr.Zero)
                {
                    RfcErrorInfo errorInfo;
                    UnsafeNativeMethods.RfcDestroyFunction(_functionHandler, out errorInfo);
                    _functionHandler = IntPtr.Zero;
                }

                // Disposing has been done.
                _disposed = true;
            }
        }

        #endregion

    }
}
