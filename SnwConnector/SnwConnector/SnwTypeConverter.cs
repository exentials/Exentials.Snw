using System;

namespace Exentials.Snw.SnwConnector
{
    internal static class SnwTypeConverter
    {
        public static RfcType ToRfcType(Type type)
        {
            if (type == typeof(char[])) return RfcType.Char;
            if (type == typeof(byte[])) return RfcType.Byte;
            if (type == typeof(string)) return RfcType.String;
            if (type == typeof(SnwNumeric)) return RfcType.Num;
            if (type == typeof(SnwDate)) return RfcType.Date;
            if (type == typeof(SnwTime)) return RfcType.Time;
            if (type == typeof(int)) return RfcType.Int;
            if (type == typeof(byte)) return RfcType.Int1;
            if (type == typeof(short)) return RfcType.Int2;
            if (type == typeof(float)) return RfcType.Float;
            if (type == typeof(double)) return RfcType.Float;
            if (type == typeof(decimal)) return RfcType.DecF34;
            if (type == typeof(SnwXString)) return RfcType.XString;
            if (type.GetInterface(typeof(IDataStructure).Name) != null) return RfcType.Structure;
            if (type.GetInterface(typeof(IDataTable).Name) != null) return RfcType.Table;
            return RfcType.Null;
        }

        public static Type ToType(RfcType type)
        {
            switch (type)
            {
                case RfcType.Char:
                    return typeof(string);
                case RfcType.Byte:
                    return typeof(byte[]);
                case RfcType.String:
                    return typeof(string);
                case RfcType.Num:
                    return typeof(SnwNumeric);
                case RfcType.Bcd:
                    return typeof(double);
                case RfcType.Date:
                    return typeof(SnwDate);
                case RfcType.Time:
                    return typeof(SnwTime);
                case RfcType.Int:
                    return typeof(int);
                case RfcType.Int1:
                    return typeof(byte);
                case RfcType.Int2:
                    return typeof(short);
                case RfcType.Float:
                    return typeof(double);
                case RfcType.DecF16:
                case RfcType.DecF34:
                    return typeof(decimal);
                case RfcType.XString:
                    return typeof(SnwXString);
                case RfcType.Structure:
                    return typeof(SnwStructure);
                case RfcType.Table:
                    return typeof(SnwTable<>);
                default: return null;
            }
        }
    }
}
