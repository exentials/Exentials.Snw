using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Exentials.Snw.SnwConnector
{
    public sealed class SnwXString 
    {
        private readonly byte[] _value;

        internal SnwXString(byte[] bytes)
        {
            _value = bytes;
        }

        public SnwXString(int length)
        {
            _value = new byte[length];
        }

        public int Length
        {
            get { return _value.Length; }
        }

        public override string ToString()
        {
            var sb = new StringBuilder(_value.Length * 2);
            foreach (var x in _value) sb.AppendFormat("{0:X}", x);
            return sb.ToString();
        }

        public static implicit operator SnwXString(byte[] bytes)
        {
            return new SnwXString(bytes);
        }

        public byte[] ToByteArray()
        {
            return _value;
        }

    }
}
