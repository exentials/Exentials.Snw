using System;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Exentials.Snw.SnwConnector.Native;

namespace Exentials.Snw.SnwConnector
{
    /// <summary>
    /// Provide functionality for setting or getting parameter value.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces")]
    public interface IDataParameter
    {
    }

    public static class DataParameterExtender
    {
        internal static IntPtr DataHandle(this IDataParameter dataContainer)
        {
            return ((IDataContainer)dataContainer).DataHandle;
        }

        /// <summary>
        /// Set the parameter value
        /// </summary>
        /// <param name="dataContainer"></param>
        /// <param name="name">Parameter name</param>
        /// <param name="paramValue">Parameter value</param>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "IDataParameter")]
        public static void SetParameter(this IDataParameter dataContainer, string name, object paramValue)
        {
            SetParameter(dataContainer, name, paramValue, 0, 0);
        }

        /// <summary>
        /// Set the parameter value
        /// </summary>
        /// <param name="dataContainer"></param>
        /// <param name="name">Parameter name</param>
        /// <param name="paramValue">Parameter value</param>
        /// <param name="length">Parameter length</param>
        /// <param name="decimals">Number of decimals</param>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "IDataParameter")]
        public static void SetParameter(this IDataParameter dataContainer, string name, object paramValue, int length, int decimals)
        {
            RfcErrorInfo errorInfo;
            var type = paramValue.GetType();

            switch (SnwTypeConverter.ToRfcType(type))
            {
                case RfcType.Char:
                    {
                        var value = (char[])paramValue;
                        UnsafeNativeMethods.RfcSetChars(dataContainer.DataHandle(), name, value, value.Length, out errorInfo);
                        errorInfo.IfErrorThrowException();
                    } break;
                case RfcType.Num:
                    {
                        var value = (SnwNumeric)paramValue;
                        if ((length > 0) && (value.Length > length)) throw new SnwConnectorException(Messages.MaxParameterLengthError, length);
                        UnsafeNativeMethods.RfcSetNum(dataContainer.DataHandle(), name, value.ToChar(), value.Length, out errorInfo);
                        errorInfo.IfErrorThrowException();
                    } break;
                case RfcType.String:
                    {
                        var value = (string)paramValue;
                        UnsafeNativeMethods.RfcSetString(dataContainer.DataHandle(), name, value, value.Length, out errorInfo);
                        errorInfo.IfErrorThrowException();
                    } break;
                case RfcType.Date:
                    {
                        var value = (SnwDate)paramValue;
                        UnsafeNativeMethods.RfcSetDate(dataContainer.DataHandle(), name, value.ToChar(), out errorInfo);
                        errorInfo.IfErrorThrowException();
                    } break;
                case RfcType.Time:
                    {
                        var time = (SnwTime)paramValue;
                        UnsafeNativeMethods.RfcSetTime(dataContainer.DataHandle(), name, time.ToChar(), out errorInfo);
                        errorInfo.IfErrorThrowException();
                    } break;
                case RfcType.Byte:
                    {
                        var value = (byte[])paramValue;
                        UnsafeNativeMethods.RfcSetBytes(dataContainer.DataHandle(), name, value, value.Length, out errorInfo);
                        errorInfo.IfErrorThrowException();
                    } break;
                case RfcType.Int:
                    {
                        UnsafeNativeMethods.RfcSetInt(dataContainer.DataHandle(), name, (int)paramValue, out errorInfo);
                        errorInfo.IfErrorThrowException();
                    } break;
                case RfcType.Int1:
                    {
                        UnsafeNativeMethods.RfcSetInt1(dataContainer.DataHandle(), name, (byte)paramValue, out errorInfo);
                        errorInfo.IfErrorThrowException();
                    }
                    break;
                case RfcType.Int2:
                    {
                        UnsafeNativeMethods.RfcSetInt2(dataContainer.DataHandle(), name, (short)paramValue, out errorInfo);
                        errorInfo.IfErrorThrowException();
                    } break;
                case RfcType.Float:
                    {
                        UnsafeNativeMethods.RfcSetFloat(dataContainer.DataHandle(), name, (double)paramValue, out errorInfo);
                        errorInfo.IfErrorThrowException();
                    }
                    break;
                case RfcType.DecF16:
                    {
                        UnsafeNativeMethods.RfcSetDecF16(dataContainer.DataHandle(), name, (decimal)paramValue, out errorInfo);
                        errorInfo.IfErrorThrowException();
                    }
                    break;
                case RfcType.DecF34:
                    {
                        UnsafeNativeMethods.RfcSetDecF34(dataContainer.DataHandle(), name, (decimal)paramValue, out errorInfo);
                        errorInfo.IfErrorThrowException();
                    }
                    break;
                case RfcType.XString:
                    {
                        var value = (SnwXString)paramValue;
                        UnsafeNativeMethods.RfcSetXString(dataContainer.DataHandle(), name, value.ToByteArray(), value.Length, out errorInfo);
                        errorInfo.IfErrorThrowException();
                    }
                    break;
                default:
                    {
                        throw new SnwConnectorException(string.Format(CultureInfo.InvariantCulture, Messages.TypeNotHandled, type));
                    }
            }

        }

        /// <summary>
        /// Get parameter value. 
        /// </summary>
        /// <typeparam name="T">Type of parameter.</typeparam>
        /// <param name="dataContainer"></param>
        /// <param name="name">Parameter name.</param>
        /// <returns>Return the parameter value.</returns>
        public static T GetParameter<T>(this IDataParameter dataContainer, string name)
        {
            return GetParameter<T>(dataContainer, name, 0, 0);
        }

        /// <summary>
        /// Get parameter value. 
        /// </summary>
        /// <typeparam name="T">Type of parameter.</typeparam>
        /// <param name="dataContainer"></param>
        /// <param name="name">Parameter name.</param>
        /// <param name="length">Parameter length.</param>
        /// <returns>Return the parameter value.</returns>
        public static T GetParameter<T>(this IDataParameter dataContainer, string name, int length)
        {
            return GetParameter<T>(dataContainer, name, length, 0);
        }

        /// <summary>
        /// Get parameter value 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataContainer"></param>
        /// <param name="name">Parameter Name</param>
        /// <param name="length">Length of parameter or 0 for non sized types (ex. SnwDate, SnwTime) </param>
        /// <param name="decimals">Number of decimal palaces</param>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "IDataParameter")]
        public static T GetParameter<T>(this IDataParameter dataContainer, string name, int length, int decimals)
        {
            IDataInfo info;
            if (dataContainer is IDataStructure)
            {
                info = SnwMetadata.GetFieldInfo((SnwStructure)dataContainer, name);
            }
            else if (dataContainer is SnwFunction)
            {
                info = SnwMetadata.GetParameterInfo((SnwFunction)dataContainer, name);
            }
            else
            {
                throw new ArgumentException();
            }
            return GetParameter<T>(dataContainer, name, info.Length, info.Decimals, info.RfcType);
        }

        /// <summary>
        /// Get parameter value 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataContainer"></param>
        /// <param name="name">Parameter Name</param>
        /// <param name="rfcType">Parameter type</param>
        /// <param name="length">Length of parameter or 0 for non sized types (ex. SnwDate, SnwTime) </param>
        /// <param name="decimals">Number of decimal palaces</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "IDataParameter")]
        public static T GetParameter<T>(this IDataParameter dataContainer, string name, int length, int decimals, RfcType rfcType)
        {
            RfcErrorInfo errorInfo;
            object result;

            switch (rfcType)
            {
                case RfcType.Char:
                    {
                        var buffer = new StringBuilder(length);
                        UnsafeNativeMethods.RfcGetChars(dataContainer.DataHandle(), name, buffer, buffer.Length, out errorInfo);
                        errorInfo.IfErrorThrowException();
                        result = buffer;
                    }
                    break;
                case RfcType.Num:
                    {
                        var buffer = new char[length];
                        UnsafeNativeMethods.RfcGetNum(dataContainer.DataHandle(), name, buffer, buffer.Length, out errorInfo);
                        errorInfo.IfErrorThrowException();
                        result = new SnwNumeric(new string(buffer));
                    }
                    break;
                case RfcType.String:
                    {
                        int stringLength = 200;
                        //UnsafeNativeMethods.RfcGetStringLength(dataContainer.DataHandle(), name, out stringLength, out errorInfo);
                        //errorInfo.IfErrorThrowException();
                        var stringBuffer = new StringBuilder(stringLength + 1);
                        int retlength;
                        UnsafeNativeMethods.RfcGetString(dataContainer.DataHandle(), name, stringBuffer, stringBuffer.Capacity, out retlength, out errorInfo);
                        errorInfo.IfErrorThrowException();
                        result = stringBuffer.ToString();
                    }
                    break;
                case RfcType.Date:
                    {
                        var date = new char[8];
                        UnsafeNativeMethods.RfcGetDate(dataContainer.DataHandle(), name, date, out errorInfo);
                        errorInfo.IfErrorThrowException();
                        result = new SnwDate(new string(date));
                    }
                    break;
                case RfcType.Time:
                    {
                        var time = new char[6];
                        UnsafeNativeMethods.RfcGetTime(dataContainer.DataHandle(), name, time, out errorInfo);
                        errorInfo.IfErrorThrowException();
                        result = new SnwTime(new string(time));
                    }
                    break;
                case RfcType.Byte:
                    {
                        var buffer = new byte[length];
                        UnsafeNativeMethods.RfcGetBytes(dataContainer.DataHandle(), name, buffer, buffer.Length, out errorInfo);
                        errorInfo.IfErrorThrowException();
                        result = buffer;
                    }
                    break;
                case RfcType.Int:
                    {
                        int value;
                        UnsafeNativeMethods.RfcGetInt(dataContainer.DataHandle(), name, out value, out errorInfo);
                        errorInfo.IfErrorThrowException();
                        result = value;
                    }
                    break;
                case RfcType.Int1:
                    {
                        byte value;
                        UnsafeNativeMethods.RfcGetInt1(dataContainer.DataHandle(), name, out value, out errorInfo);
                        errorInfo.IfErrorThrowException();
                        result = value;
                    }
                    break;
                case RfcType.Int2:
                    {
                        short value;
                        UnsafeNativeMethods.RfcGetInt2(dataContainer.DataHandle(), name, out value, out errorInfo);
                        errorInfo.IfErrorThrowException();
                        result = value;
                    }
                    break;
                case RfcType.Float:
                    {
                        double value;
                        UnsafeNativeMethods.RfcGetFloat(dataContainer.DataHandle(), name, out value, out errorInfo);
                        errorInfo.IfErrorThrowException();
                        result = value;
                    }
                    break;
                case RfcType.DecF16:
                    {
                        decimal value;
                        UnsafeNativeMethods.RfcGetDecF16(dataContainer.DataHandle(), name, out value, out errorInfo);
                        errorInfo.IfErrorThrowException();
                        result = value;
                    }
                    break;
                case RfcType.DecF34:
                    {
                        decimal value;
                        UnsafeNativeMethods.RfcGetDecF16(dataContainer.DataHandle(), name, out value, out errorInfo);
                        errorInfo.IfErrorThrowException();
                        result = value;
                    }
                    break;
                case RfcType.XString:
                    {
                        var buffer = new byte[length];
                        int outLength;
                        UnsafeNativeMethods.RfcGetXString(dataContainer.DataHandle(), name, buffer, buffer.Length,
                                                          out outLength, out errorInfo);
                        errorInfo.IfErrorThrowException();
                        result = new SnwXString(buffer);
                    }
                    break;
                case RfcType.Structure:
                    {
                        IntPtr structureHandle;
                        UnsafeNativeMethods.RfcGetStructure(dataContainer.DataHandle(), name, out structureHandle, out errorInfo);
                        errorInfo.IfErrorThrowException();
                        var structure = new SnwStructure(structureHandle);
                        result = typeof(T).GetConstructor(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public, null, new[] { typeof(SnwStructure) }, null).Invoke(new object[] { structure });
                    }
                    break;
                case RfcType.Table:
                    {
                        IntPtr tableHandle;
                        UnsafeNativeMethods.RfcGetTable(dataContainer.DataHandle(), name, out tableHandle, out errorInfo);
                        errorInfo.IfErrorThrowException();
                        var table = new SnwTable<SnwStructure>(tableHandle);
                        result = typeof(T).GetConstructor(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public, null, new[] { typeof(SnwTable<SnwStructure>) }, null).Invoke(new object[] { table });
                    }
                    break;
                default:
                    {
                        throw new SnwConnectorException(string.Format(CultureInfo.InvariantCulture, Messages.TypeNotHandled, typeof(T)));
                    }
            }
            return (T)result;
        }
    }
}
