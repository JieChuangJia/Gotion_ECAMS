using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ECAMSDataAccess;
using ECAMSModel;

namespace ECAMSPresenter
{
    public class ControlTaskEventArgs:EventArgs
    {
        public long[] ControlTaskIDArr{get;set;}
        public bool[] ControlTaskStatus { get; set; }
    }

    public class InStoreByHandEventArgs : EventArgs
    {
        public string TrayIDs { get; set; }
        public string TaskTypeName { get; set; }
    }

    public class QueryCtrlTaskEventArgs : EventArgs
    {
        public string CtrlTaskName { get; set; }
        public string CtrlTaskStatus { get; set; }
        public string StoreHouseName { get; set; }
        public string TaskCreateMode { get; set; }
        public string TaskType { get; set; }
    }

    public interface IControlTaskView:IBaseView
    {
        #region 事件
        /// <summary>
        /// 窗体初始化刷新管理任务列表
        /// </summary>
        event EventHandler eventLoadData;

        event EventHandler<AutoRefreshEventArgs> eventAutoRefresh;

        event EventHandler<ControlTaskEventArgs> eventCancelTask;

        event EventHandler<ControlTaskEventArgs> eventCompleteByHand;

        event EventHandler<InStoreByHandEventArgs> eventInStoreByHand;

        /// <summary>
        /// 退出事件
        /// </summary>
        event EventHandler eventExit;

        /// <summary>
        /// 条件查询控制任务
        /// </summary>
        event EventHandler<QueryCtrlTaskEventArgs> eventQueryCtrlTask;
        #endregion

        #region 方法
        /// <summary>
        /// 刷新控制任务
        /// </summary>
        /// <param name="taskModel"></param>
        //void ShowControlTaskData(List<ControlTaskModel> taskModelList);
        /// <summary>
        /// 刷新控制任务
        /// </summary>
        /// <param name="taskModel"></param>
        void ShowControlTaskData(DataTable dtTask);
        /// <summary>
        /// 弹出是否请求对话框
        /// </summary>
        /// <param name="content"></param>
        /// <returns>返回：0是，1：否</returns>
        int AskMessBox(string content);

        void ShowMessage(string titleStr, string contentStr);
        #endregion

    }
}
