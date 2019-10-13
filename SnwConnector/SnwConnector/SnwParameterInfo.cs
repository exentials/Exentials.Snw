using Exentials.Snw.SnwConnector.Native;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Exentials.Snw.SnwConnector
{
    public sealed class SnwParameterInfo : IDataInfo
    {
        private readonly IntPtr _handle;

        internal SnwParameterInfo(RfcParameterDesc parameterDesc)
        {
            Direction = parameterDesc.Direction;
            RfcType = parameterDesc.Type;
            Name = parameterDesc.Name;
            Length = parameterDesc.Length;
            Decimals = parameterDesc.Decimals;
            UnicodeLength = parameterDesc.UCLength;
            IsOptional = (parameterDesc.Optional == 1);
            Description = parameterDesc.ParameterText;
            DefaultValue = parameterDesc.DefaultValue;
            IsStructure = (RfcType == RfcType.Structure);
            IsTable = (RfcType == RfcType.Table);
            _handle = parameterDesc.TypeDescHandle;

            if (IsStructure || IsTable)
            {
                RfcErrorInfo errorInfo;
                var bufferName = new StringBuilder(31);
                UnsafeNativeMethods.RfcGetTypeName(_handle, bufferName, out errorInfo);
                errorInfo.IfErrorThrowException();
                TypeName = bufferName.ToString();
            }
        }

        public RfcDirection Direction { get; private set; }

        public RfcType RfcType { get; private set; }

        public string Name { get; private set; }

        public int Length { get; private set; }

        public int Decimals { get; private set; }

        public int UnicodeLength { get; private set; }

        public bool IsOptional { get; private set; }

        public string Description { get; private set; }

        public string DefaultValue { get; private set; }

        public string TypeName { get; private set; }

        public bool IsStructure { get; private set; }

        public bool IsTable { get; private set; }

        public Type GetParameterType()
        {
            return SnwTypeConverter.ToType(RfcType);
        }

        internal IntPtr Handle
        {
            get { return _handle; }
        }

    }
}
