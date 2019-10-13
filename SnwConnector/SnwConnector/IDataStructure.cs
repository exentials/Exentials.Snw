using System.Diagnostics.CodeAnalysis;

namespace Exentials.Snw.SnwConnector
{
    /// <summary>
    /// Identify a data structure type
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces")]
    internal interface IDataStructure : IDataParameter
    {
    }
}
