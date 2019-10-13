using System;
using System.Linq;
using System.Globalization;

namespace Exentials.Snw.SnwConnector
{
    /// <summary>
    /// SnwDate handle the SAP stored date format HHmmss
    /// </summary>
    public struct SnwTime : IComparable, IComparable<SnwTime>, IEquatable<SnwTime>
    {
        private string _value;

        internal SnwTime(string value)
        {
            _value = value;
        }

        private string Value
        {
            get
            {
                if (string.IsNullOrEmpty(_value)) _value = "000000";
                return _value;
            }
        }

        public TimeSpan ToTimeSpan()
        {
            return ConvertToTimeSpan(_value);
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
            return (obj is SnwTime) && Value.Equals(((SnwTime)obj).Value);
        }

        public bool Equals(SnwTime other)
        {
            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return int.Parse(Value, CultureInfo.InvariantCulture);
        }

        #region Operators overloading

        public static implicit operator TimeSpan(SnwTime value)
        {
            return value.ToTimeSpan();
        }

        public static implicit operator SnwTime(TimeSpan value)
        {
            return new SnwTime(string.Format(CultureInfo.InvariantCulture, "{0:HHmmss}", value));
        }

        public static implicit operator SnwTime(string value)
        {
            ConvertToTimeSpan(value);
            return new SnwTime(value);
        }

        private static TimeSpan ConvertToTimeSpan(string value)
        {
            if (value.Length != 6) throw new ArgumentOutOfRangeException("value");
            var time = new TimeSpan(
                int.Parse(value.Substring(0, 2), CultureInfo.InvariantCulture),
                int.Parse(value.Substring(2, 2), CultureInfo.InvariantCulture),
                int.Parse(value.Substring(4, 2), CultureInfo.InvariantCulture)
            );
            return time;
        }

        public static implicit operator string(SnwTime data)
        {
            return data.Value;
        }

        public static bool operator ==(SnwTime date1, SnwTime date2)
        {
            return (date1.GetHashCode() == date2.GetHashCode());
        }

        public static bool operator !=(SnwTime date1, SnwTime date2)
        {
            return (date1.GetHashCode() != date2.GetHashCode());
        }

        public static bool operator <(SnwTime date1, SnwTime date2)
        {
            return (date1.GetHashCode() < date2.GetHashCode());
        }

        public static bool operator >(SnwTime date1, SnwTime date2)
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
            if (!(obj is SnwTime))
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Messages.ArgumentMustBeOfType, typeof(SnwTime)));
            }
            var snwTime = (SnwTime)obj;
            return this < snwTime ? -1 : (this > snwTime ? 1 : 0);
        }

        public int CompareTo(SnwTime other)
        {
            return this < other ? -1 : (this > other ? 1 : 0);
        }
    }
}
