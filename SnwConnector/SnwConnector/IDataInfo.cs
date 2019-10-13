namespace Exentials.Snw.SnwConnector
{
    public interface IDataInfo
    {
        RfcType RfcType { get; }
        string Name { get; }
        int Length { get; }
        int Decimals { get; }
    }
}
