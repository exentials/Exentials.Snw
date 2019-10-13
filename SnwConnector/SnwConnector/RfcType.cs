namespace Exentials.Snw.SnwConnector
{
    public enum RfcType
    {
        /// <summary>
        /// 1-byte or multibyte character, fixed size, blank padded
        /// </summary>
        Char = 0,
        /// <summary>
        /// Date ( YYYYYMMDD )
        /// </summary>
        Date = 1,
        /// <summary>
        /// Packed number, any length between 1 and 16 bytes
        /// </summary>
        Bcd = 2,
        /// <summary>
        /// Time (HHMMSS) 
        /// </summary>
        Time = 3,
        /// <summary>
        /// Raw data, binary, fixed length, zero padded.
        /// </summary>
        Byte = 4,
        /// <summary>
        /// Internal table
        /// </summary>
        Table = 5,
        /// <summary>
        /// Digits, fixed size, leading '0' padded.
        /// </summary>
        Num = 6,
        /// <summary>
        /// Floating point, double precission
        /// </summary>
        Float = 7,
        /// <summary>
        /// 4-byte integer
        /// </summary>
        Int = 8,
        /// <summary>
        /// 2-byte integer. Obsolete, not directly supported by ABAP/4
        /// </summary>
        Int2 = 9,
        /// <summary>
        /// 1-byte integer, unsigned. Obsolete, not directly supported by ABAP/4
        /// </summary>
        Int1 = 10,
        /// <summary>
        /// Not supported data type.
        /// </summary>
        Null = 14,
        /// <summary>
        /// ABAP structure
        /// </summary>
        Structure = 17,
        /// <summary>
        /// IEEE 754r decimal floating point, 8 bytes
        /// </summary>
        DecF16 = 23,
        /// <summary>
        /// IEEE 754r decimal floating point, 16 bytes
        /// </summary>
        DecF34 = 24,
        /// <summary>
        /// No longer used!
        /// </summary>
        XmlData = 28,
        /// <summary>
        /// Variable-length, null-terminated string
        /// </summary>
        String = 29,
        /// <summary>
        /// Variable-length raw string, length in bytes    
        /// </summary>
        XString = 30,
    }
}
