using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSDataAccess;
using System.Data;
namespace ECAMSPresenter
{
    public class ProductTracePresenter : BasePresenter<IProductTraceView>
    {
        #region 全局变量
        PalletHistoryRecordBll palletTrace = new PalletHistoryRecordBll();
        #endregion
        public ProductTracePresenter(IProductTraceView view)
            :base(view)
        {

        }
        protected override void OnViewSet()
        {
            View.eventPalletTrace += this.PalletTraceQueryHandler;

        }
        #region 事件响应
        private void PalletTraceQueryHandler(object sender, PalletTraceEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.palletID))
            {
                View.DispQueryResult(null,null);
                return;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", typeof(int));
            dt.Columns.Add("时间", typeof(string));
            dt.Columns.Add("当前生产流程段", typeof(string));
            dt.Columns.Add("详细", typeof(string));
            dt.Columns.Add("登录用户", typeof(string));
            int count = 1;
            List<PalletHistoryRecordModel> palletEventList = palletTrace.GetEventList(e.palletID, e.sortReAsc);
            if (palletEventList == null || palletEventList.Count < 1)
            {
                View.DispQueryResult(null,null);
                return;
            }
            foreach (PalletHistoryRecordModel traceModel in palletEventList)
            {
                if (traceModel == null)
                {
                    continue;
                }
                dt.Rows.Add(count++, traceModel.hisEventTime.ToString("yyyy-MM-dd HH:mm:ss"), traceModel.processStatus, traceModel.hisEventDetail,traceModel.currentUser);
            }
            View.DispQueryResult(e.palletID,dt);
        }
        #endregion
    }
}
