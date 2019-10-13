using System;

namespace Exentials.Snw.SnwConnector
{
    /// <summary>
    /// Supports data handle container.
    /// </summary>
    internal interface IDataContainer
    {
        IntPtr DataHandle { get; }
    }
}
