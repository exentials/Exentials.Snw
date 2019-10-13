namespace Exentials.Snw.SnwConnector
{
    public enum RfcDirection
    {
        /// <summary>
        /// Value not initialized
        /// </summary>
        None = 0x00,
        /// <summary>
        /// Import parameter. This corresponds to ABAP IMPORTING parameter.
        /// </summary>
        Import = 0x01,
        /// <summary>
        /// Export parameter. This corresponds to ABAP EXPORTING parameter.
        /// </summary>
        Export = 0x02,
        /// <summary>
        /// Import and export parameter. This corresponds to ABAP CHANGING parameter.
        /// </summary>
        Changing = Import | Export,
        /// <summary>
        /// Table parameter. This corresponds to ABAP TABLES parameter.
        /// </summary>
        Tables = 0x04 | Changing
    }
}
