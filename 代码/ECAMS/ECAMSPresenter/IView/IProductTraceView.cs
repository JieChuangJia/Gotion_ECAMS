using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace ECAMSPresenter
{
    /// <summary>
    /// 托盘追踪事件参数
    /// </summary>
    public class PalletTraceEventArgs : EventArgs
    {
        public string palletID { get; set; }
        public bool sortReAsc { get; set; } //是否按时间正序排序
    }
    public interface IProductTraceView:IBaseView
    {
        #region 方法
        void DispQueryResult(string palletID,DataTable dt);

        //void ClearDisp();
        #endregion
        #region 事件
        event EventHandler<PalletTraceEventArgs> eventPalletTrace;
        #endregion

    }
}
