using System;
using System.Linq;
using System.Globalization;

namespace Exentials.Snw.SnwConnector
{
    /// <summary>
    /// SnwDate handle the SAP stored date format yyyyMMdd
    /// </summary>
    public struct SnwDate : IComparable, IComparable<SnwDate>, IEquatable<SnwDate>
    {
        private readonly string _value;

        internal SnwDate(string value)
        {
            _value = value;
        }

        private string Value
        {
            get { return _value ?? Empty._value; }
        }

        public bool IsEmpty
        {
            get { return (_value == null) || (_value == Empty._value); }
        }

        public DateTime ToDateTime()
        {
            return IsEmpty ? new DateTime() : DateTime.ParseExact(_value, "yyyyMMdd", CultureInfo.InvariantCulture);
        }

        internal char[] ToChar()
        {
            return _value.ToArray();
        }

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            return (obj is SnwDate) && Value.Equals(((SnwDate)obj).Value);
        }

        public bool Equals(SnwDate other)
        {
            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return int.Parse(Value, CultureInfo.InvariantCulture);
        }

        #region Operators overloading

        public static implicit operator DateTime(SnwDate value)
        {
            return value.ToDateTime();
        }

        public static implicit operator SnwDate(DateTime value)
        {
            return value == DateTime.MinValue ? Empty : new SnwDate(value.ToString("yyyyMMdd", CultureInfo.InvariantCulture));
        }

        public static implicit operator SnwDate(string value)
        {
            // Check if value is a regular date
            if (value != Empty.Value)
                DateTime.ParseExact(value, "yyyyMMdd", CultureInfo.InvariantCulture);
            return new SnwDate(value);
        }

        public static implicit operator string(SnwDate data)
        {
            return data.Value;
        }

        public static bool operator ==(SnwDate date1, SnwDate date2)
        {
            return (date1.GetHashCode() == date2.GetHashCode());
        }

        public static bool operator !=(SnwDate date1, SnwDate date2)
        {
            return (date1.GetHashCode() != date2.GetHashCode());
        }

        public static bool operator <(SnwDate date1, SnwDate date2)
        {
            return (date1.GetHashCode() < date2.GetHashCode());
        }

        public static bool operator >(SnwDate date1, SnwDate date2)
        {
            return (date1.GetHashCode() > date2.GetHashCode());
        }

        #endregion

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            if (!(obj is SnwDate))
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Messages.ArgumentMustBeOfType, typeof(SnwDate)));
            }
            var snwDate = (SnwDate)obj;
            return this < snwDate ? -1 : (this > snwDate ? 1 : 0);
        }

        public int CompareTo(SnwDate other)
        {
            return this < other ? -1 : (this > other ? 1 : 0);
        }

        public static readonly SnwDate Empty = new SnwDate("00000000");
    }
}
