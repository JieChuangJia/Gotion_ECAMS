using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSDataAccess;
using System.Threading;
using System.Data;

namespace ECAMSPresenter
{
    public class ManageTaskPresenter:BasePresenter<IManageTaskView>
    {

        #region 全局变量
        ECAMSDataAccess.ManageTaskBll bllManaTask = new ECAMSDataAccess.ManageTaskBll();    // 管理任务逻辑
        Thread autoRefreshThread = null;                                                    //自动刷新线程
        int autoInterval = 2000;                                                             //自动刷新间隔
        bool shouldAutoStop = false;   
        #endregion

        #region 初始化
        public ManageTaskPresenter(IManageTaskView view)
            : base(view)
        {
            autoRefreshThread = new Thread(new ThreadStart(AutoRereshData));
            autoRefreshThread.IsBackground = true;
        }

        protected override void OnViewSet()
        {
            this.View.eventLoadData += LoadDataEventHandler;
            this.View.eventAutoRefrsh += AutoRefreshEventHandler;
            this.View.eventExit += ExitEventHandler;
            this.View.eventRefreshTask += RefreshTaskEventHandler;
        }
        #endregion

        #region 实现IManageTaskView事件函数
        private void RefreshTaskEventHandler(object sender, EventArgs e)
        {
            //List<ManageTaskModel> manageTaskList = bllManaTask.GetModelList("");

            //this.View.ShowManageTaskData(manageTaskList);

            DataSet dsMana = bllManaTask.GetManaTaskList();
            if (dsMana != null && dsMana.Tables.Count > 0)
            {
                this.View.ShowManageTaskData(dsMana.Tables[0]);
            }
        }

        private void ExitEventHandler(object sender, EventArgs e)
        {
            try
            {
                shouldAutoStop = true;
                autoRefreshThread.Abort();
            }
            catch
            { }
        }

        private void LoadDataEventHandler(object sender, EventArgs e)
        {
            //List<ManageTaskModel> manageTaskList = bllManaTask.GetModelList("");

            //this.View.ShowManageTaskData(manageTaskList);
          
            DataSet dsMana = bllManaTask.GetManaTaskList();
            if (dsMana != null && dsMana.Tables.Count > 0)
            {
                this.View.ShowManageTaskData(dsMana.Tables[0]);
            }
        }

        private void AutoRefreshEventHandler(object sender, AutoRefreshEventArgs e)
        {
            if (e.isAutoRefresh == true)
            {
                if (autoRefreshThread.ThreadState == (ThreadState.Unstarted | ThreadState.Background))
                {
                    autoRefreshThread.Start();
                }
                else
                {
                    shouldAutoStop = false;
                    autoRefreshThread.Resume();
                } 
            }
            else
            {
                shouldAutoStop = true;
                autoRefreshThread.Suspend();
            }
        }
        #endregion

        #region 私有方法
        private void AutoRereshData()
        {
            while (!shouldAutoStop)
            {
                Thread.Sleep(autoInterval);
                //List<ManageTaskModel> manageTaskList = bllManaTask.GetModelList("");
                //this.View.ShowManageTaskData(manageTaskList);
                DataSet dsMana = bllManaTask.GetManaTaskList();
                if (dsMana != null && dsMana.Tables.Count > 0)
                {
                    this.View.ShowManageTaskData(dsMana.Tables[0]);
                }
            }
        }
        #endregion

    }
}
