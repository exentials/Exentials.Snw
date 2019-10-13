// This code was generated automatically by Exentials SnwConnector
namespace Exentials.Snw.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Exentials.Snw;
    using Exentials.Snw.SnwConnector;
    using Exentials.Snw.Functions;
    using Exentials.Snw.Structures;
    
    
    public sealed class BapiTransactionRollback : Exentials.Snw.SnwConnector.SnwFunction, Exentials.Snw.SnwConnector.IExport<BapiTransactionRollback.ExportParameters>
    {
        
        private ExportParameters _export;
        
        public BapiTransactionRollback(Exentials.Snw.SnwConnector.SnwConnection connection) : 
                base("BAPI_TRANSACTION_ROLLBACK", connection)
        {
        }
        
        public ExportParameters Export
        {
            get
            {
                if ((this._export == null))
                {
                    this._export = new ExportParameters(this);
                }
                return this._export;
            }
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public sealed class ExportParameters : Exentials.Snw.SnwConnector.SnwParametersContainer
        {
            
            private Bapiret2 _return;
            
            public ExportParameters(BapiTransactionRollback container) : 
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
                    if ((this._return == null))
                    {
                        this._return = this.GetParameter<Bapiret2>("RETURN", 548, 0);
                    }
                    return this._return;
                }
            }
        }
    }
}
