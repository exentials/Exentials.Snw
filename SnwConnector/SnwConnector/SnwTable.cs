using Exentials.Snw.SnwConnector.Native;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Exentials.Snw.SnwConnector
{
    public sealed class SnwTable<T> : IDataContainer, IDataTable, IEnumerable<T> where T : SnwStructure
    {
        private IList<T> _snwStructureList;
        private readonly IntPtr _tableHandle;

        internal SnwTable(IntPtr dataHandle)
        {
            _tableHandle = dataHandle;
        }

        public SnwTable(SnwTable<SnwStructure> table)
        {
            if (table == null) throw new ArgumentNullException("table");
            _tableHandle = (table as IDataContainer).DataHandle;
        }

        IntPtr IDataContainer.DataHandle
        {
            get { return _tableHandle; }
        }

        private void EnsureValues()
        {
            int count;
            RfcErrorInfo errorInfo;

            UnsafeNativeMethods.RfcGetRowCount(_tableHandle, out count, out errorInfo);
            errorInfo.IfErrorThrowException();

            UnsafeNativeMethods.RfcMoveToFirstRow(_tableHandle, out errorInfo);
            for (var i = 0; i < count; i++)
            {
                var structureHandle = UnsafeNativeMethods.RfcGetCurrentRow(_tableHandle, out errorInfo);
                var structure = (T)typeof(T).GetConstructor(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public, null, new [] { typeof(SnwStructure) }, null).Invoke(new object[] { new SnwStructure(structureHandle) });
                _snwStructureList.Add(structure);
                UnsafeNativeMethods.RfcMoveToNextRow(_tableHandle, out errorInfo);
            }
        }

        public T AddRow()
        {
            RfcErrorInfo errorInfo;
            var structureHandle = UnsafeNativeMethods.RfcAppendNewRow(_tableHandle, out errorInfo);
            var structure = (T)typeof(T).GetConstructor(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public, null, new [] { typeof(SnwStructure) }, null).Invoke(new object[] { new SnwStructure(structureHandle) });
            return structure;
        }

        public void Clear()
        {
            RfcErrorInfo errorInfo;
            UnsafeNativeMethods.RfcDeleteAllRows(_tableHandle, out errorInfo);
            _snwStructureList.Clear();
            errorInfo.IfErrorThrowException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_snwStructureList == null)
            {
                _snwStructureList = new List<T>();
                EnsureValues();
            }
            return _snwStructureList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
