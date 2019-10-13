namespace Exentials.Snw.SnwConnector
{
    /// <summary>
    /// Define a property to exchange Tables parameters.
    /// </summary>
    /// <typeparam name="T">The type containing tables parameters.</typeparam>
    public interface ITables<out T> where T : SnwParametersContainer
    {
        T Tables { get; }
    }
}
