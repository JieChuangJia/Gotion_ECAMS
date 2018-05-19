using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace WCSTest
{
    public interface IDBMonitorView
    {
        /// <summary>
        /// 刷新db1，db2
        /// </summary>
        /// <param name="devID"></param>
        /// <param name="dtDB1"></param>
        /// <param name="dtDB2"></param>
        void RefreshPlcData(int devID, DataTable dtDB1, DataTable dtDB2);
    }
}
