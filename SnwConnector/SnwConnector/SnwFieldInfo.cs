using System;
using System.Diagnostics.CodeAnalysis;

namespace Exentials.Snw.SnwConnector
{
    public sealed class SnwFieldInfo : IDataInfo
    {
        internal SnwFieldInfo(RfcFieldDesc field)
        {
            Name = field.Name;
            Length = field.NUCLength;
            Decimals = field.Decimals;
            UnicodeLength = field.UCLength;
            RfcType = field.Type;
        }

        public string Name { get; private set; }

        public int Length { get; private set; }

        public int Decimals { get; private set; }

        public int UnicodeLength { get; private set; }

        public RfcType RfcType { get; private set; }

        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public Type GetFieldType()
        {
            return SnwTypeConverter.ToType(this.RfcType);
        }

    }
}
