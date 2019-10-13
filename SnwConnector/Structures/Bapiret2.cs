// This code is generated automatically by Exentials SnwConnector
namespace Exentials.Snw.Structures
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Exentials.Snw;
    using Exentials.Snw.Functions;
    using Exentials.Snw.SnwConnector;
    using Exentials.Snw.Structures;


    [Exentials.Snw.SnwConnector.SnwStructureAttribute("BAPIRET2")]
    public sealed class Bapiret2 : Exentials.Snw.SnwConnector.SnwStructure
    {

        public Bapiret2(Exentials.Snw.SnwConnector.SnwStructure structure) :
            base(structure)
        {
        }

        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public string Type
        {
            get
            {
                return this.GetParameter<string>("TYPE", 1, 0);
            }
            set
            {
                this.SetParameter("TYPE", value, 1, 0);
            }
        }

        public string Id
        {
            get
            {
                return this.GetParameter<string>("ID", 20, 0);
            }
            set
            {
                this.SetParameter("ID", value, 20, 0);
            }
        }

        public Exentials.Snw.SnwConnector.SnwNumeric Number
        {
            get
            {
                return this.GetParameter<Exentials.Snw.SnwConnector.SnwNumeric>("NUMBER", 3, 0);
            }
            set
            {
                this.SetParameter("NUMBER", value, 3, 0);
            }
        }

        public string Message
        {
            get
            {
                return this.GetParameter<string>("MESSAGE", 220, 0);
            }
            set
            {
                this.SetParameter("MESSAGE", value, 220, 0);
            }
        }

        public string LogNo
        {
            get
            {
                return this.GetParameter<string>("LOG_NO", 20, 0);
            }
            set
            {
                this.SetParameter("LOG_NO", value, 20, 0);
            }
        }

        public Exentials.Snw.SnwConnector.SnwNumeric LogMsgNo
        {
            get
            {
                return this.GetParameter<Exentials.Snw.SnwConnector.SnwNumeric>("LOG_MSG_NO", 6, 0);
            }
            set
            {
                this.SetParameter("LOG_MSG_NO", value, 6, 0);
            }
        }

        public string MessageV1
        {
            get
            {
                return this.GetParameter<string>("MESSAGE_V1", 50, 0);
            }
            set
            {
                this.SetParameter("MESSAGE_V1", value, 50, 0);
            }
        }

        public string MessageV2
        {
            get
            {
                return this.GetParameter<string>("MESSAGE_V2", 50, 0);
            }
            set
            {
                this.SetParameter("MESSAGE_V2", value, 50, 0);
            }
        }

        public string MessageV3
        {
            get
            {
                return this.GetParameter<string>("MESSAGE_V3", 50, 0);
            }
            set
            {
                this.SetParameter("MESSAGE_V3", value, 50, 0);
            }
        }

        public string MessageV4
        {
            get
            {
                return this.GetParameter<string>("MESSAGE_V4", 50, 0);
            }
            set
            {
                this.SetParameter("MESSAGE_V4", value, 50, 0);
            }
        }

        public string Parameter
        {
            get
            {
                return this.GetParameter<string>("PARAMETER", 32, 0);
            }
            set
            {
                this.SetParameter("PARAMETER", value, 32, 0);
            }
        }

        public int Row
        {
            get
            {
                return this.GetParameter<int>("ROW", 4, 0);
            }
            set
            {
                this.SetParameter("ROW", value, 4, 0);
            }
        }

        public string Field
        {
            get
            {
                return this.GetParameter<string>("FIELD", 30, 0);
            }
            set
            {
                this.SetParameter("FIELD", value, 30, 0);
            }
        }

        public string System
        {
            get
            {
                return this.GetParameter<string>("SYSTEM", 10, 0);
            }
            set
            {
                this.SetParameter("SYSTEM", value);
            }
        }
    }
}
