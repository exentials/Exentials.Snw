// This code was generated automatically by Exentials SnwConnector
namespace Exentials.Snw.Functions
{
    using Exentials.Snw.SnwConnector;
    using Exentials.Snw.Structures;


    public sealed class BapiTransactionCommit : Exentials.Snw.SnwConnector.SnwFunction, Exentials.Snw.SnwConnector.IImport<BapiTransactionCommit.ImportParameters>, Exentials.Snw.SnwConnector.IExport<BapiTransactionCommit.ExportParameters>
    {

        private ImportParameters _import;

        private ExportParameters _export;

        public BapiTransactionCommit(Exentials.Snw.SnwConnector.SnwConnection connection) :
                base("BAPI_TRANSACTION_COMMIT", connection)
        {
        }

        public ImportParameters Import
        {
            get
            {
                if ((_import == null))
                {
                    _import = new ImportParameters(this);
                }
                return _import;
            }
        }

        public ExportParameters Export
        {
            get
            {
                if ((_export == null))
                {
                    _export = new ExportParameters(this);
                }
                return _export;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public sealed class ImportParameters : Exentials.Snw.SnwConnector.SnwParametersContainer
        {

            public ImportParameters(BapiTransactionCommit container) :
                    base(container)
            {
            }

            /// <summary>
            /// Using the command `COMMIT AND WAIT`
            /// </summary>
            public string Wait
            {
                get
                {
                    return this.GetParameter<string>("WAIT", 1, 0);
                }
                set
                {
                    this.SetParameter("WAIT", value, 1, 0);
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public sealed class ExportParameters : Exentials.Snw.SnwConnector.SnwParametersContainer
        {

            private Bapiret2 _return;

            public ExportParameters(BapiTransactionCommit container) :
                    base(container)
            {
            }

            /// <summary>
            /// Return Messages
            /// </summary>
            public Bapiret2 Return
            {
                get
                {
                    if ((_return == null))
                    {
                        _return = this.GetParameter<Bapiret2>("RETURN", 548, 0);
                    }
                    return _return;
                }
            }
        }
    }
}
