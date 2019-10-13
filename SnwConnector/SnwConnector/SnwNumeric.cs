using System;
using System.Linq;
using System.Globalization;

namespace Exentials.Snw.SnwConnector
{
    /// <summary>
    /// Represent a SAP Numc type.
    /// <para>
    /// It is a numeric string width fixed length and zero left padded.
    /// </para>
    /// </summary>
    public sealed class SnwNumeric : IComparable, IComparable<SnwNumeric>, IEquatable<SnwNumeric>
    {
        private int _value;
        private int _length;

        public SnwNumeric(int length)
        {
            _value = 0;
            _length = length;
        }

        public SnwNumeric(int value, int length)
        {
            _value = value;
            _length = length;
        }

        public SnwNumeric(string value)
        {
            _value = int.Parse(value, CultureInfo.InvariantCulture);
            _length = value.Length;
        }

        public void Add(int value)
        {
            _value += value;
        }

        public int Length
        {
            get { return _length; }
        }

        public static implicit operator SnwNumeric(string value)
        {
            return new SnwNumeric(value);
        }

        public static explicit operator char[](SnwNumeric value)
        {
            return value.ToString().ToCharArray();
        }

        public static bool operator <(SnwNumeric numeric1, SnwNumeric numeric2)
        {
            return (numeric1._value < numeric2._value);
        }

        public static bool operator >(SnwNumeric numeric1, SnwNumeric numeric2)
        {
            return (numeric1._value > numeric2._value);
        }

        public static bool operator ==(SnwNumeric numeric1, SnwNumeric numeric2)
        {
            return (numeric1._value == numeric2._value);
        }

        public static bool operator !=(SnwNumeric numeric1, SnwNumeric numeric2)
        {
            return (numeric1._value != numeric2._value);
        }

        public override int GetHashCode()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            return _value.Equals(obj);
        }

        public bool Equals(SnwNumeric other)
        {
            return (this == other);
        }

        internal char[] ToChar()
        {
            return ToString().ToArray();
        }

        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture).PadLeft(_length, '0');
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            var num = obj as SnwNumeric;
            if (num == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Messages.ArgumentMustBeOfType, typeof(SnwNumeric)));
            }
            return this < num ? -1 : (this > num ? -1 : 0);
        }

        public int CompareTo(SnwNumeric other)
        {
            return this < other ? -1 : (this > other ? -1 : 0);
        }
    }
}
