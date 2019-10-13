namespace Exentials.Snw.SnwConnector
{
    /// <summary>
    /// Define a property to Import parameters.
    /// </summary>
    /// <typeparam name="T">The type containing import parameters.</typeparam>
    public interface IImport<out T> where T : SnwParametersContainer
    {
        T Import { get; }
    }
}
