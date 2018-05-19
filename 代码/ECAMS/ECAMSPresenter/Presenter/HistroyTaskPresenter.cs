using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using ECAMSDataAccess;

namespace ECAMSPresenter
{
    public class HistroyTaskPresenter : BasePresenter<IHistoryTaskQueryView>
    {
        #region 全局变量
        private readonly HistoryManageTaskBll bllHisManaTask = new HistoryManageTaskBll(); //历史任务逻辑
        #endregion

        #region 初始化
        public HistroyTaskPresenter(IHistoryTaskQueryView view)
            : base(view)
        {

        }

        protected override void OnViewSet()
        {
            this.View.eventIniTasktypeCom += IniTasktypeComEventHandler;
            this.View.eventQueryTask += QueryTaskEventHandler;

        }

        #endregion

        #region 实现IManageTaskView事件函数
        private void IniTasktypeComEventHandler(object sender, EventArgs e)
        {
            DataSet dsCom = bllHisManaTask.GetDistinctTaskTypeName();
            this.View.IniTaskTypeNameCom(dsCom);
        }

        private void QueryTaskEventHandler(object sender, QueryHisTaskEventArgs e)
        {
           
           bool openProgress =  this.View.OpenProgressBar();
           if (openProgress)
           {
               this.View.SetExitButtonEnabled(false);
               List<HistoryManageTaskModel> hisTaskList = bllHisManaTask.GetModeListByFactor(e.StartTime,
                     e.EndTime, e.TasktypeName, e.TasktypeNameChecked, e.StartPosition, e.StartPositionChecked, e.EndPostion, e.EndPostionChecked);

               this.View.ShowHisTaskData(hisTaskList);
               this.View.CloseProgressBar();
               this.View.SetExitButtonEnabled(true);
           }
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
