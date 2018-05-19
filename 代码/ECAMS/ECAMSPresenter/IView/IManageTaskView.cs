using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ECAMSDataAccess;

namespace ECAMSPresenter
{
    public class AutoRefreshEventArgs : EventArgs
    {
        public bool isAutoRefresh { get; set; }
    }

    public class ManaTaskEventArgs :EventArgs
    {
        public long ManaTaskID {get;set;}
    }

    public interface IManageTaskView:IBaseView
    {
        #region 事件
        /// <summary>
        /// 窗体初始化刷新管理任务列表
        /// </summary>
        event EventHandler eventLoadData;

        /// <summary>
        /// 自动刷新事件
        /// </summary>
        event EventHandler<AutoRefreshEventArgs> eventAutoRefrsh;

        /// <summary>
        /// 退出事件
        /// </summary>
        event EventHandler eventExit;

        event EventHandler eventRefreshTask;

        event EventHandler<ManaTaskEventArgs> eventDeleteTask;
        #endregion

        #region 方法
        /// <summary>
        /// 刷新管理任务
        /// </summary>
        /// <param name="taskModel"></param>
        void ShowManageTaskData(List<ECAMSDataAccess.ManageTaskModel> taskModelList);
        /// <summary>
        /// 刷新管理任务
        /// </summary>
        /// <param name="taskModel"></param>
        void ShowManageTaskData(DataTable dtManaList);
        #endregion

    }
}
