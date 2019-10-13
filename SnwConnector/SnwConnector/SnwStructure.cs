using System;

namespace Exentials.Snw.SnwConnector
{
    public class SnwStructure : IDataStructure, IDataContainer
    {
        private readonly IntPtr _dataHandle;

        internal SnwStructure(IntPtr dataHandle)
        {
            _dataHandle = dataHandle;
        }

        public SnwStructure(SnwStructure structure)
        {
            _dataHandle = structure._dataHandle;
        }
      
        IntPtr IDataContainer.DataHandle
        {
            get { return _dataHandle; }
        }
    }
}
