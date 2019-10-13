using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Globalization;

namespace Exentials.Snw.SnwConnector
{
    [Serializable]
    public class SnwConnectorException : Exception
    {
        private readonly RfcRc _rfcRc;
        private readonly string _message;

        public SnwConnectorException()
        {
        }

        public SnwConnectorException(string message)
            : base(message)
        {
        }

        public SnwConnectorException(string message, params object[] args)
            : base(string.Format(CultureInfo.InvariantCulture, message, args))
        {
        }

        public SnwConnectorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected SnwConnectorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info", "No SerializationInfo");
            }
        }

        internal SnwConnectorException(RfcRc rfcRc)
        {
            _rfcRc = rfcRc;
        }

        internal SnwConnectorException(RfcErrorInfo errorInfo)
        {
            _rfcRc = errorInfo.Code;
            _message = errorInfo.Message;
        }
        
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public override string Message
        {
            get
            {
                switch (_rfcRc)
                {
                    case RfcRc.RfcCommunicationFailure: return Messages.RFC_COMMUNICATION_FAILURE;
                    case RfcRc.RfcLogonFailure: return Messages.RFC_LOGON_FAILURE;
                    case RfcRc.RfcAbapRuntimeFailure: return string.Format(CultureInfo.InvariantCulture, Messages.RFC_ABAP_RUNTIME_FAILURE, _message);
                    case RfcRc.RfcAbapException: return Messages.RFC_ABAP_EXCEPTION;
                    case RfcRc.RfcInvalidParameter: return Messages.RFC_INVALID_PARAMETER;
                    default: return base.Message;
                }
            }
        }
    }
}
