using Exentials.Snw.SnwConnector.Native;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Exentials.Snw.SnwConnector
{
    public static class SnwMetadata
    {
        public static IEnumerable<SnwParameterInfo> GetParameters(SnwFunction function)
        {
            var parameters = new List<SnwParameterInfo>();
            int count;

            RfcErrorInfo errorInfo;
            UnsafeNativeMethods.RfcGetParameterCount(function.DescriptionHandle, out count, out errorInfo);
            errorInfo.IfErrorThrowException();

            for (int i = 0; i < count; i++)
            {
                RfcParameterDesc parameterDescription;
                UnsafeNativeMethods.RfcGetParameterDescByIndex(function.DescriptionHandle, i, out parameterDescription, out errorInfo);
                parameters.Add(new SnwParameterInfo(parameterDescription));
            }
            return parameters.ToArray();
        }

        public static IEnumerable<SnwFieldInfo> GetStructureFields(SnwParameterInfo structure)
        {
            var fields = new List<SnwFieldInfo>();
            int count;

            RfcErrorInfo errorInfo;
            UnsafeNativeMethods.RfcGetFieldCount(structure.Handle, out count, out errorInfo);
            errorInfo.IfErrorThrowException();

            for (int i = 0; i < count; i++)
            {
                RfcFieldDesc fieldDescription;
                UnsafeNativeMethods.RfcGetFieldDescByIndex(structure.Handle, i, out fieldDescription, out errorInfo);
                fields.Add(new SnwFieldInfo(fieldDescription));
            }

            return fields.ToArray();
        }

        public static SnwParameterInfo GetParameterInfo(SnwFunction function, string parameterName)
        {
            RfcParameterDesc parameterDescription;
            RfcErrorInfo errorInfo;
            UnsafeNativeMethods.RfcGetParameterDescByName(function.DescriptionHandle, parameterName, out parameterDescription, out errorInfo);
            errorInfo.IfErrorThrowException();
            return new SnwParameterInfo(parameterDescription);
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static SnwFieldInfo GetFieldInfo(SnwStructure structure, string fieldName)
        {
            RfcFieldDesc fieldDescription;
            RfcErrorInfo errorInfo;
            UnsafeNativeMethods.RfcGetFieldDescByName(structure.DataHandle(), fieldName, out fieldDescription, out errorInfo);
            errorInfo.IfErrorThrowException();
            return new SnwFieldInfo(fieldDescription);
        }
    }
}
