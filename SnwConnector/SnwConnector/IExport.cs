namespace Exentials.Snw.SnwConnector
{
    /// <summary>
    /// Define a property to Export parameters.
    /// </summary>
    /// <typeparam name="T">The type containing export parameters.</typeparam>
    public interface IExport<out T> where T : SnwParametersContainer
    {
        T Export { get; }
    }
}
