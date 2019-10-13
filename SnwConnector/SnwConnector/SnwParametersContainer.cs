using System;

namespace Exentials.Snw.SnwConnector
{
    public abstract class SnwParametersContainer : IDataParameter, IDataContainer
    {
        private readonly IntPtr _containerHandle;

        protected SnwParametersContainer(SnwFunction container)
        {
            _containerHandle = container.DataHandle();
        }

        IntPtr IDataContainer.DataHandle
        {
            get { return _containerHandle; }
        }
    }
}
